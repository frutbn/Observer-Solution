using NUnit.Framework;
using UnityEngine;

public sealed class ObserverSolutionTests
{
    private TestChannel _testChannel;
    private TestMessage? _testMessage;

    [SetUp]
    public void OnInitialize()
    {
        _testChannel = ScriptableObject.CreateInstance<TestChannel>();
        _testMessage = null;
    }
    
    [Test]
    public void TryAddListener_TestMethod_ReturnsTrue()
    {
        Assert.IsTrue(_testChannel.TryAddListener(OnSendTest));
    }

    [Test]
    public void TryRemoveListener_TestMethod_ReturnsTrue()
    {
        _testChannel.TryAddListener(OnSendTest);
        _testChannel.Send(new TestMessage
        {
            Int = 200
        });
        Assert.IsTrue(_testChannel.TryRemoveListener(OnSendTest));
        _testChannel.Send(new TestMessage
        {
            Int = 300
        });
        
        Assert.IsNotNull(_testMessage);
        Assert.AreEqual(200, _testMessage.Value.Int);
    }

    [Test]
    public void Send_TestMessage_EqualsSameValues()
    {
        _testChannel.TryAddListener(OnSendTest);
        Assert.IsNull(_testMessage);
        _testChannel.Send(new TestMessage
        {
            Int = 100
        });
        Assert.IsNotNull(_testMessage);
        Assert.AreEqual(100, _testMessage.Value.Int);
    }

    private void OnSendTest(TestMessage testMessage)
    {
        _testMessage = testMessage;
    }

    private sealed class TestChannel : BaseScriptableChannel<TestMessage>
    {
    }
    
    private struct TestMessage : IMessage
    {
        public int Int;
    }
}