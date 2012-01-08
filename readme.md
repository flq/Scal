## Scal

__Scal__ uses the OSS projects [Caliburn.Micro][1], [StructureMap][2], [MemBus][3] and [DynamicXaml][4] to provide a WPF dev experience
that is similar to Caliburn.Micro but gives you a couple more things into your hand:

* One of the greatest DI Containers there are that gives you a lot more injection power than MEF
* An Event Aggregator that allows you to configure many aspects of moving messages from publishers to subscribers
* An API to create XAML Object trees via code and ways to access and use XAML-based resources

Check out the Sample App for usage guideline. The major difference is that you should include 

  [1]: http://caliburnmicro.codeplex.com
  [2]: https://github.com/structuremap/structuremap
  [3]: https://github.com/flq/MemBus
  [4]: https://github.com/flq/XamlTags