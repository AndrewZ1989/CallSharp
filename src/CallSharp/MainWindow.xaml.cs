﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallSharp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MethodDatabase methodDatabase = new MethodDatabase();

    public ObservableHashSet<string> Candidates
    {
      get { return (ObservableHashSet<string>)GetValue(CandidatesProperty); }
      set { SetValue(CandidatesProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Candidates.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CandidatesProperty =
        DependencyProperty.Register("Candidates", typeof(ObservableHashSet<string>), typeof(MainWindow), 
          new PropertyMetadata(new ObservableHashSet<string>()));

    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
    {
      Candidates.Clear();
      string input = TbIn.Text;
      string output = TbOut.Text;


      foreach (
        var m in methodDatabase.FindOneToOneNonStatic(typeof(string), typeof(string)))
      {
        string actualOutput = m.InvokeWithSingleArgument(input) as string;
        if (output.Equals(actualOutput))
          Candidates.Add("input." + m.Name + "(output)");
      }
    }
  }
}
