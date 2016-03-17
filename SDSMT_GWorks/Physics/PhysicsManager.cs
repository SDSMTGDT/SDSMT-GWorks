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

        private Dictionary<Collidable, List<CollisionGroup>> collidableToGroups;

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
        /// Returns the collision event publisher associated with a given collidable.
        /// If the publisher does not exist, a new one will be created.
        /// </summary>
        /// <param name="collider">The object that will be colliding with others</param>
        /// <returns>A collision publisher associated with the collider</returns>
        public CollisionEventPublisher getCollisionPublisher(Collidable collider)
        {
            if (publishers[collider] == null)
                publishers[collider] = new CollisionEventPublisher(eventManager, this, collider);
            return publishers[collider];            
        }

        private List<CollisionGroup> getCollisionGroups(Collidable c)
        {
            if (collidableToGroups[c] == null)
                collidableToGroups[c] = new List<CollisionGroup>();
            return collidableToGroups[c];
        }


        public CollisionGroup createCollisionGroup(string name, CollisionStructureFactory factory)
        {
            CollisionStructure structure = factory.createCollisionStructure();
            CollisionGroup group = new CollisionGroup(collisionGroupCounter, name, structure);
            collisionGroupCounter++;
            return group;
        }
        
        public void registerCollidableInGroup(Collidable c, CollisionGroup group)
        {
            group.structure.insert(c);
            getCollisionGroups(c).Add(group);
        }

        public void unregisterCollidableFromGroup(Collidable c, CollisionGroup group)
        {
            group.structure.delete(c);
            getCollisionGroups(c).Remove(group);
        }

        private void unregisterCollidableFromAllGroups(Collidable c)
        {
            foreach(CollisionGroup group in getCollisionGroups(c))
            {
                group.structure.delete(c);
            }
            collidableToGroups.Remove(c);
        }

        public void unregisterCollidableFromSystem(Collidable c)
        {
            publishers[c]?.Dispose();
            unregisterCollidableFromAllGroups(c);
        }

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
            publishers[collider]?.publish(collided);
            publishers[collided]?.publish(collider);
        }
    }
}
