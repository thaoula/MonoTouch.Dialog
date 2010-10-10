using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace Sample
{
	public class DemoStyleController: DialogViewController
	{
		public DemoStyleController () : base(null)
		{
			StyleSheet.Default.Add("Root", new ElementStyle 
			                       { 
										BackgroundColor = UIColor.FromRGBA(150, 150, 150, 220), 
										TextColor = UIColor.Black, 
										TextFont = UIFont.SystemFontOfSize(14f),
										DetailColor = UIColor.Orange,
										TextBackgroundColor = UIColor.Clear,
										DetailBackgroundColor = UIColor.Clear
									});
			
			StyleSheet.Default.Add("Heading", new ElementStyle 
			                       { 
										TextFont = UIFont.BoldSystemFontOfSize(20f),
										BackgroundColor = UIColor.FromRGBA(150, 250, 150, 180), 
								   });
			StyleSheet.Default.Add("Radio", new ElementStyle { TextFont = UIFont.ItalicSystemFontOfSize(14f), TextColor = UIColor.Red });
			StyleSheet.Default.Add("Green", new ElementStyle { DetailColor = UIColor.Green, TextColor = UIColor.Green });
			
			ConfigureRoot();
			
		}
		
		private void ConfigureRoot()
		{
			TableView.BackgroundColor = UIColor.Clear;

			var root = new RootElement("Home")
			{
				
				new Section()
				{
					new StringElement("Heading Element") { CssClass = "heading" }
				},
				new Section()
				{
					new ImageStringElement("Detailed Item", "This should be orange", null),
					new ImageStringElement("Detailed Item", "This should be all green", null) { CssClass = "Green" },
				},
				new Section()
				{
					new RootElement("Radio Items", new RadioGroup(0))
					{
						new Section("", "", "Radio")
						{
						new RadioElement("Radio Item 1"),
						new RadioElement("Radio Item 2"),
						new RadioElement("This should be big") { CssClass = "Heading" }
					
						}
					}
				}
			};
			
			root.CssClass = "Root";
			Root = root;
		}
		
		public override void ViewWillAppear (bool animated)
		{
			NavigationController.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("free-apple-wallpapers.jpg"));
			base.ViewWillAppear (animated);
		}
	}
	
	
	
	public partial class AppDelegate
	{
		private void DemoStyled()
		{
			var dvc = new DemoStyleController();
			navigation.PushViewController (dvc, true);
		}
			
	}
}

