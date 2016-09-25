using SDSMTGDT.GWorks.Events;
using SDSMTGDT.GWorks.Physics.Collisions;
using SDSMTGDT.GWorks.Physics.Collisions.DataStructures;
using SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics
{
    /// <summary>
    /// Handles moving bodies and their collisions.
    /// Interfaces with the EventManager to provide collision information to the outside world
    /// </summary>
    public class CollisionManager
    {
        /// <summary>
        /// The event manager to forward collision events to.
        /// </summary>
        public EventManager eventManager { get; private set; }

        /// <summary>
        /// Mapping between collidable objects and the groups they are in.
        /// Its faster maintiaining this dictionary than calling contains on every group.
        /// </summary>
        private Dictionary<Collidable, List<CollisionGroup>> collidableToGroups;

        /// <summary>
        /// Returns the number of collidables in the current collisions manager.
        /// </summary>
        public int collidableCount
        {
            get { return collidableToGroups.Count; }
        }

        /// <summary>
        /// Each collision group has a unique id.
        /// </summary>
        private uint collisionGroupCounter;

        /// <summary>
        /// Create a new CollisionManager which publishes to the given EventManager
        /// </summary>
        /// <param name="eventManager">The EventManager to publish collision information to</param>
        public CollisionManager(EventManager eventManager)
        {
            this.eventManager = eventManager;
            this.collidableToGroups = new Dictionary<Collidable, List<CollisionGroup>>();
            this.collisionGroupCounter = 0;
        }

        /// <summary>
        /// Creates a collision group with a name, unique id, backed by a specific data structure.
        /// </summary>
        /// <param name="name">Name of the collision group</param>
        /// <param name="factory">Factory which provides backing to the collision group</param>
        /// <returns>A new Collision Group.</returns>
        public CollisionGroup createCollisionGroup(string name, CollisionStructureFactory factory)
        {
            CollisionStructure structure = factory.createCollisionStructure();
            CollisionGroup group = new CollisionGroup(collisionGroupCounter, name, structure);
            collisionGroupCounter++;
            return group;
        }
        
        /// <summary>
        /// Inserts a Collidable into a collision group and records that the collidable
        /// is in that group
        /// </summary>
        /// <param name="c">The collidable to add to the group</param>
        /// <param name="group">The group to add the collidable to</param>
        public void registerCollidableInGroup(Collidable c, CollisionGroup group)
        {
            group.structure.insert(c);
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups))
            {
                groups = new List<CollisionGroup>();
                collidableToGroups[c] = groups;
            }
            groups.Add(group);
        }

        /// <summary>
        /// Removes a collidable from a collision group if the collision group contains the collidable.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="group"></param>
        /// <returns>Whether or not the collidable was found in the group and removed.</returns>
        public bool unregisterCollidableFromGroup(Collidable c, CollisionGroup group)
        {
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups) || !groups.Contains(group))
                return false;
            // Remove the mapping from the collidable to the group
            groups.Remove(group);

            // If this group was the last group this colliable was associated with, remove the mapping entirely
            if (groups.Count == 0)
            {
                collidableToGroups.Remove(c);
            }

            // Remove the collidable from the group
            return group.structure.delete(c);
        }

        /// <summary>
        /// Removes a collidable from all collision groups it was registered to.
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        private void unregisterCollidableFromAllGroups(Collidable c)
        {
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups))
                return;

            // Remove the collidable from each of its collision groups
            foreach(CollisionGroup group in groups)
            {
                group.structure.delete(c);
            }

            // Remove the mapping from the collidable to its collision groups
            collidableToGroups.Remove(c);
        }

        /// <summary>
        /// Removes a collidable from the collisions system.
        /// And unregisters the collidable from its collision groups.
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        public void unregisterCollidable(Collidable c)
            {
            unregisterCollidableFromAllGroups(c);
        }

        /// <summary>
        /// Collides all registered collidables
        /// </summary>
        public void checkAllCollisions()
        {
            foreach (var keyValuePair in collidableToGroups)
            {
                foreach (var collidableGroup in keyValuePair.Value)
                {
                    collideWithGroup(keyValuePair.Key, collidableGroup);
                }
            }
        }

        /// <summary>
        /// Collides a collidable with all of its registered collidable groups
        /// </summary>
        /// <param name="c">The collidable to check</param>
        public bool checkCollisions(Collidable c)
        {
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups))
                return false;
            foreach(CollisionGroup group in groups)
            {
                collideWithGroup(c, group);
            }
            return true;
        }

        public bool updateLocation(Collidable c)
        {
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups))
                return false;
            foreach (CollisionGroup group in groups)
            {
                group.structure.update(c);
            }
            return true;
        }

        /// <summary>
        /// Finds the collidables currently engaged with a given collidable
        /// </summary>
        /// <param name="c">A collidable to check</param>
        /// <returns>A list of other collidables c is colliding with</returns>
        public List<Collidable> listCurentCollisions(Collidable c)
        {
            List<CollisionGroup> groups;
            List<Collidable> collisions = new List<Collidable>();
            if (!collidableToGroups.TryGetValue(c, out groups))
                return collisions;
            foreach (CollisionGroup group in groups)
            {
                collisions.AddRange(group.structure.checkCollision(c));
            }
            return collisions;
        }

        /// <summary>
        /// Collides a collidable with all registered collidables in a given collision group.
        /// The collidable does not have to be registered with the collision group.
        /// </summary>
        /// <param name="c">The collidable to check</param>
        /// <param name="group">The group of other collidables to check</param>
        public void collideWithGroup(Collidable c, CollisionGroup group)
        {
            foreach(Collidable other in group.structure.checkCollision(c))
            {
                collide(c, other, group);
            }            
        }

        /// <summary>
        /// Used to publish collision events between two collidables.
        /// Note: The collisions are symmetric and will be called on each object.
        /// </summary>
        /// <param name="collider">The first object in the collision</param>
        /// <param name="collided">The second object in the collision</param>
        /// <param name="group">The group containing one or both collidables</param>
        internal void collide(Collidable collider, Collidable collided, CollisionGroup group)
        {
            collided.collisionPublisher?.publish(collider, group);
            collider.collisionPublisher?.publish(collided, group);
        }
    }
}
