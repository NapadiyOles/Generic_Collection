using NUnit.Framework;
using System;
using Generic_Stack;

namespace Stack_Tests
{
    public class Tests
    {
        Stack<int> _ints;

        [Test]
        public void PeekCheck_WhenStackIsEmpty_ThrowsInvalidOperation()
        {
            _ints = new Stack<int>();

            Assert.That(() => _ints.Peek(),
                Throws.TypeOf<InvalidOperationException>()
                    .With.Message.EqualTo("Stack is empty"));
        }

        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(10)]
        public void PeekCheck_WhenStackIsNotEmpty_ReturnsLastObject(int item)
        {

            _ints = new Stack<int>(new int[] {item});

            int actualItem = _ints.Peek();

            int[] expectedArray = new int[1];

            _ints.CopyTo(expectedArray, 0);

            Assert.AreEqual(expectedArray[0], actualItem);
        }

        [Test]
        public void PopCheck_WhenStackIsEmpty_ThrowsInvalidOperation()
        {
            _ints = new Stack<int>();

            Assert.That(() => _ints.Pop(),
                Throws.TypeOf<InvalidOperationException>()
                    .With.Message.EqualTo("Stack is empty"));
        }

        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(10)]
        public void PopCheck_WhenStackContainsOneObject_DeletesAndReturnsDeletedObject(int item)
        {
            _ints = new Stack<int>(new int[] {item});

            Assert.AreEqual(item, _ints.Pop());

            Assert.That(() => _ints.Peek(),
                Throws.TypeOf<InvalidOperationException>()
                    .With.Message.EqualTo("Stack is empty"));
        }

        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        [TestCase(-10, 0)]
        [TestCase(0, -10)]
        public void PopCheck_WhenStackContainsMoreThenOneObject_DeletesAndReturnsDeletedObject(int firstItem, int secondItem)
        {
            _ints = new Stack<int>(new int[] {firstItem, secondItem});

            Assert.AreEqual(secondItem, _ints.Pop());

            Assert.AreEqual(firstItem, _ints.Peek());
        }

        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(10)]
        public void PushCheck_WhenStackIsEmpty_AddsObject(int item)
        {
            _ints = new Stack<int>();

            _ints.Push(item);

            int[] array = new int[1];

            _ints.CopyTo(array, 0);

            Assert.AreEqual(
                item,
                array[0]);
        }

        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        [TestCase(-10, 0)]
        [TestCase(0, -10)]
        public void PushCheck_WhenStackIsNotEmpty_AddsObject(int firstItem, int secondItem)
        {
            _ints = new Stack<int>(new int[] {firstItem});

            _ints.Push(secondItem);

            int[] expectedArray = new int[]{firstItem, secondItem};

            int[] actualArray = new int[2];

            _ints.CopyTo(actualArray, 0);

            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void SwapCheck_WhenStackDoesNotContainEnoughObjects_ThrowsInvalidOperation()
        {
            _ints = new Stack<int>();

            Assert.That(() => _ints.Swap(),
                Throws.TypeOf<InvalidOperationException>()
                    .With.Message.EqualTo("Not enough items"));
        }

        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        [TestCase(-10, 0)]
        [TestCase(0, -10)]
        public void SwapCheck_WhenStackContainsEnoughObjects_ReplacesTwoLastAmongThemselves(int firstItem, int secondItem)
        {
            int[] expectedArray = { firstItem, secondItem };

            _ints = new Stack<int>(expectedArray);

            _ints.Swap();

            (expectedArray[0], expectedArray[1]) = (expectedArray[1], expectedArray[0]);

            int[] actualArray = new int[2];

            _ints.CopyTo(actualArray, 0);

            Assert.AreEqual(expectedArray, actualArray);
        }

        [TestCase(new int[]{1, 2, 3})]
        public void EnumeratorCheck_ReturnsActualElementsOfTheStack(int[] expectedArray)
        {
            _ints = new Stack<int>(expectedArray);

            int index = 0;

            foreach (var item in _ints)
            {
                Assert.AreEqual(expectedArray[expectedArray.Length - ++index], item);
            }
        }
    }
}