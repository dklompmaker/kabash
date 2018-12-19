# Kabash Observables
[travis-ci-status]: https://img.shields.io/travis/AgeOfLearning/kabash.svg
[nuget-status]: https://img.shields.io/nuget/vpre/Kabash.svg

[![travis][travis-ci-status]](https://travis-ci.org/AgeOfLearning/kabash)
[![nuget][nuget-status]](https://www.nuget.org/packages/Kabash/)

An observable library for multi-way data binding. 

## Basics

```csharp
var observable = new Observable<string>();
observable.ValueChanged += OnValueChanged;
observable.Value = "Hello World";

// Also supports implicit conversion...
var observable = (Observable<string>)"Hello World";

private void OnValueChanged(IObservable<string> sender, ObservedValueChangedEventArgs<string> e)
{
    Console.WriteLine(e.PreviousValue);
    Console.WriteLine(e.CurrentValue);
}

```

## Mirroring For Data-Binding

You can mirror another observable to change the value immediately when the mirrored observable changes. You can remove this binding via `UnMirror`.

```csharp
var a = new Observable<float>();
a.Value = 10;

var b = new Observable<float>();
b.Mirror(a);

a.Value = 20;

// Both equal 20...
Assert.AreEqual(a.Value, b.Value);
```

## When Queries

You can invoke callbacks upon the observable value reaching a specific query. These will be invoked immediately if the query passes matches. These callbacks will remain for the lifetime of the observable unless removed with `RemoveQuery`.

```csharp
var a = new Observable<float>();
a.When(v => v == 10, OnWhenCriteriaMet);
a.Value = 5;

// Callback triggered...
a.Value = 10;

private void OnWhenCriteriaMet(IObservable<float> sender, ObservedCriteriaMetEventArgs<float> e)
{
    Console.WriteLine(sender.Value);
}
```

## IOC Compliant

The library is IOC compliant. You can implement the `IObservableFactory` for providing a means to resolve an unknown observable at runtime. If in Unity, you will run into AOT issues if you go this route. The solution is to ensure that you reference the implemented signature at least once so it gets compiled.

## ObservableList
Use the ObservabeList to get callbacks when elements are added, removed, and modifed via this[index].

```csharp
var listA = new ObservableList<string>();
var listB = new ObservableList<string>();

// Mirror everyting that happens with listB.
listA.Mirror(listB);
listB.Add("Hello World");

// Writes "Hello World"...
Console.WriteLine(listA[0]);

// True because the same element is on both lists...
Assert.AreEqual(listA[0], listB[0]);
```