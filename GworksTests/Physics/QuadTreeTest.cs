using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDSMTGDT.GWorks.Physics.Collisions;
using NUnit.Framework;
using SDSMTGDT.GWorks.Events;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories;

namespace SDSMTGDT.GWorks.Physics
{
    /// <summary>
    /// This test tests the insertion, deletion, and boundary search methods
    /// of the CollisionQuadTree.
    /// </summary>
    [TestFixture]
    class QuadTreeTest
    {
        EventManager testEventManager;
        CollisionManager testPhysicsManager;

        /// <summary>
        /// Performs set-up operations necessary before each test.
        /// </summary>
        [SetUp]
        public void setup()
        {
            testEventManager = new EventManager();
            testPhysicsManager = new CollisionManager(testEventManager);
        }

        [Test]
        public void testQuadTreeInsert()
        {
            var collisionGroup = 
                testPhysicsManager.CreateCollisionGroup("testGroup",
                new CollisionQuadTreeFactory(0, 0, 1000));
            TestColliderA colliderA = new TestColliderA(testPhysicsManager,
                new Rectangle(500, 500, 100, 100));
            TestColliderB colliderB = new TestColliderB(testPhysicsManager,
                new Rectangle(500, 500, 50, 50));
            testPhysicsManager.RegisterCollidableInGroup(colliderA, collisionGroup);
            testPhysicsManager.RegisterCollidableInGroup(colliderB, collisionGroup);

            testPhysicsManager.CheckCollisions(colliderA);

            Assert.IsTrue(colliderA.collided);
            Assert.IsTrue(colliderB.collided);
        }

        [Test]
        public void testQuadTreeDelete()
        {
            var collisionGroup =
                testPhysicsManager.CreateCollisionGroup("testGroup",
                new CollisionQuadTreeFactory(0, 0, 1000));
            TestColliderA colliderA = new TestColliderA(testPhysicsManager,
                new Rectangle(500, 500, 100, 100));
            TestColliderB colliderB = new TestColliderB(testPhysicsManager,
                new Rectangle(500, 500, 50, 50));
            testPhysicsManager.RegisterCollidableInGroup(colliderA, collisionGroup);
            testPhysicsManager.RegisterCollidableInGroup(colliderB, collisionGroup);

            testPhysicsManager.UnregisterCollidable(colliderA);
            Assert.AreEqual(1, testPhysicsManager.CollidableCount);
            testPhysicsManager.UnregisterCollidable(colliderB);

            Assert.AreEqual(0, testPhysicsManager.CollidableCount);
        }

        [Test]
        public void testQuadTreeSplit()
        {
            int testValue = 500;
            int increment = 1;

            var collisionGroup =
                testPhysicsManager.CreateCollisionGroup("testGroup",
                new CollisionQuadTreeFactory(0, 0, 1000));

            List<BoundsCollidable> colliders = new List<BoundsCollidable>();

            for ( int i = 0; i < testValue; i += increment)
            {
                BoundsCollidable tempCollidable;
                if (i % 2 == 0)
                {
                    tempCollidable = new TestColliderA(testPhysicsManager,
                        new Rectangle(i, i, 3, 3));
                    colliders.Add(tempCollidable);
                    testPhysicsManager.
                        RegisterCollidableInGroup(tempCollidable, collisionGroup);
                }
                else
                {
                    tempCollidable = new TestColliderB(testPhysicsManager,
                        new Rectangle(i, i, 3, 3));
                    colliders.Add(tempCollidable);
                    testPhysicsManager.
                        RegisterCollidableInGroup(tempCollidable, collisionGroup);
                }
            }

            foreach( BoundsCollidable col in colliders)
            {
                testPhysicsManager.CheckCollisions(col);
            }
            foreach( BoundsCollidable col in colliders)
            {
                Assert.IsTrue(col.collided);
            }
        }
    }
    /*{
        private CollisionQuadTree root;
        /// <summary>
        /// Performs set-up operations necessary before each test.
        /// </summary>
        [SetUp]
        public void setup()
        {
            root = new CollisionQuadTree(0, 0, 1000);
        }

        [Test]
        public void testQuadTreeInsertAndSplit()
        {
            int testValue = 500;
            int increment = 10;
            BoundsCollidable bigObject =
                new BoundsCollidable(new Rectangle(0, 0, 1000, 1000));
            IEnumerable<Collidable> collided = new List<Collidable>();
            // place things on the diagonal
            for (int i = 0; i < testValue; i += increment)
            {
                root.insert(new BoundsCollidable(new Rectangle(i, i, 3, 3)));
                collided = root.checkCollision(bigObject);
                Console.WriteLine("i = " + i);
                Console.WriteLine((int)collided.Count<Collidable>());
            }
            //If testvalue isn't divisible by increment, off by one error due to integer math
            Assert.AreEqual(testValue/ increment, collided.Count<Collidable>());
        }
    }*/
}
