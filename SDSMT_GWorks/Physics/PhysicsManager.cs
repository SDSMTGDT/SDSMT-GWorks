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
    public class PhysicsManager
    {
        /// <summary>
        /// The event manager to forward collision events to.
        /// </summary>
        private EventManager eventManager;

        /// <summary>
        /// Mapping between collidable objects and the publishers for their collision events
        /// </summary>
        private Dictionary<Collidable, CollisionEventPublisher> publishers;

        /// <summary>
        /// Returns the number of event hooks active in the physics manager.
        /// </summary>
        public int eventHookCount
        {
            get { return publishers.Count; }
        }

        /// <summary>
        /// Mapping between collidable objects and the groups they are in.
        /// Its faster maintiaining this dictionary than calling .contains on every group.
        /// </summary>
        private Dictionary<Collidable, List<CollisionGroup>> collidableToGroups;

        /// <summary>
        /// Returns the number of collidables in the current physics manager.
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
        /// Create a new PhysicsManager which publishes to the given EventManager
        /// </summary>
        /// <param name="eventManager">The EventManager to publish collision information to</param>
        public PhysicsManager(EventManager eventManager)
        {
            this.eventManager = eventManager;
            this.publishers = new Dictionary<Collidable, CollisionEventPublisher>();
            this.collidableToGroups = new Dictionary<Collidable, List<CollisionGroup>>();
            this.collisionGroupCounter = 0;
        }

        /// <summary>
        /// Returns the collision event hook associated with a given collidable.
        /// If the publisher does not exist, a new one will be created.
        /// </summary>
        /// <param name="collider">The object that will be colliding with others</param>
        /// <returns>A collision publisher associated with the collider</returns>
        public GameEventHook<CollisionEventInfo> getCollisionHook(Collidable collider)
        {
            CollisionEventPublisher publisher;
            if (!publishers.TryGetValue(collider, out publisher))
            {
                publisher = new CollisionEventPublisher(eventManager, this, collider);
                publishers[collider] = publisher;
            }
            return publisher;
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
            groups.Remove(group);
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

            foreach(CollisionGroup group in groups)
            {
                group.structure.delete(c);
            }
            collidableToGroups.Remove(c);
        }

        /// <summary>
        /// Removes a collidable from the physics system. Disposes of the associated publisher.
        /// And unregisters the collidable from its collision groups.
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        public void unregisterCollidableFromSystem(Collidable c)
        {
            CollisionEventPublisher publisher;
            if (publishers.TryGetValue(c, out publisher))
            {
                publisher.Dispose();
                publishers.Remove(c);
            }
            unregisterCollidableFromAllGroups(c);
        }

        /// <summary>
        /// Collides a collidable with all of its registered collidable groups
        /// </summary>
        /// <param name="c">The collidable to check</param>
        public void checkCollisions(Collidable c)
        {
            List<CollisionGroup> groups;
            if (!collidableToGroups.TryGetValue(c, out groups))
                return;
            foreach(CollisionGroup group in groups)
            {
                collideWithGroup(c, group);
            }
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
                collide(c, other);
            }            
        }

        /// <summary>
        /// Used to publish collision events between two collidables.
        /// Note: The collisions are symmetric and will be called on each object.
        /// </summary>
        /// <param name="collider">The first object in the collision</param>
        /// <param name="collided">The second object in the collision</param>
        internal void collide(Collidable collider, Collidable collided)
        {
            CollisionEventPublisher colliderPub, collidedPub;
            if (publishers.TryGetValue(collider, out colliderPub))
                colliderPub.publish(collided);
            if (publishers.TryGetValue(collided, out collidedPub))
                collidedPub.publish(collider);
        }
    }
}
