namespace ktsu.io.Extensions.Test;

using System.Reflection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[TestClass]
public class TestReflection : TestReflectionBase
{
	[TestMethod]
	public void TestBase()
	{
		var type = typeof(TestReflection);
		string methodName = nameof(TestStatic);
		var bindingFlags = BindingFlags.Public | BindingFlags.Static;
		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);
		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
	}

	[TestMethod]
	public void TestVirtualStatic()
	{
		var type = typeof(TestReflection);
		string methodName = nameof(TestStatic);
		var bindingFlags = BindingFlags.Public | BindingFlags.Static;
		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);
		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
	}

	[TestMethod]
	public void TestNegative()
	{
		var type = typeof(TestReflection);
		string methodName = nameof(TestVirtualStatic);
		var bindingFlags = BindingFlags.Public | BindingFlags.Static;
		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);
		Assert.IsFalse(result);
		Assert.IsNull(methodInfo);
	}
}

public abstract class TestReflectionBase
{
	public static void TestStatic()
	{
	}
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
