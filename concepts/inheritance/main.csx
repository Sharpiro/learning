var parent = new Parent();
WriteLine("Calling 'Parent' as parent:");
parent.TestMethod();
WriteLine();

var unknownChild = new HiddenImplicitChild();
Parent unknownChildAsParent = new HiddenImplicitChild();
WriteLine("Calling 'HiddenImplicitChild(no new)' as child:");
unknownChild.TestMethod();
WriteLine("Calling 'HiddenImplicitChild(no new)' as parent:");
unknownChildAsParent.TestMethod();
WriteLine();

var newChild = new HiddenExplicitChild();
Parent newChildAsParent = new HiddenExplicitChild();
WriteLine("Calling 'HiddenExplicitChild(new)' as child:");
newChild.TestMethod();
WriteLine("Calling 'HiddenExplicitChild(new)' as parent:");
newChildAsParent.TestMethod();
WriteLine();

var overrideChild = new OverrideChild();
Parent overrideChildAsParent = new OverrideChild();
WriteLine("Calling 'OverrideChild' as child:");
overrideChild.TestMethod();
WriteLine("Calling 'OverrideChild' as parent:");
overrideChildAsParent.TestMethod();

class Parent
{
    public virtual void TestMethod() => WriteLine("\texecuted parent...");
}

class HiddenImplicitChild : Parent
{
    public void TestMethod() => WriteLine("\texecuted child...");
}

class HiddenExplicitChild : Parent
{
    public new void TestMethod() => WriteLine("\texecuted child...");
}

class OverrideChild : Parent
{
    public override void TestMethod() => WriteLine("\texecuted child...");
}