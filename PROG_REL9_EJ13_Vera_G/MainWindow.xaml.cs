using System;
using System.Collections.Generic;
using System.Linq;
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
// ----------
using Microsoft.Win32;
using System.IO;

namespace PROG_REL9_EJ13_Vera_G
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			cmbFuentes.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			cmbTamanos.ItemsSource = new List<int>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
		}

		private void mitSalir_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void mitAbrir_Click(object sender, RoutedEventArgs e)
		{
			AbrirFichero();
		}

		private void AbrirFichero()
		{
			string ruta = string.Empty;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Text File | *.txt";
			ofd.DefaultExt = ".txt";

			Nullable<bool> result = ofd.ShowDialog();

			if (result == true)
			{
				ruta = ofd.FileName;
				FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
				StreamReader sr = new StreamReader(fs, Encoding.Default);

				string texto = sr.ReadToEnd();

				rtxTexto.Document.Blocks.Clear();
				rtxTexto.Document.Blocks.Add(new Paragraph(new Run(texto)));
			}
		}

		private void mitNuevo_Click(object sender, RoutedEventArgs e)
		{
			rtxTexto.Document.Blocks.Clear();
		}

		private void cmbFuentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//cmbFuentes.SelectedItem = cmbFuentes.Items.CurrentItem.ToString();
			if (cmbFuentes.SelectedItem != null)
				rtxTexto.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFuentes.SelectedItem);
		}

		private void cmbTamanos_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				rtxTexto.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbTamanos.SelectedItem.ToString());
			}
			catch (Exception)
			{
				
			}
		}

		private void Abrir(object sender, ExecutedRoutedEventArgs e)
		{
			AbrirFichero();
		}

		private void Guardar(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
			if (dlg.ShowDialog() == true)
			{
				FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
				TextRange rango = new TextRange(rtxTexto.Document.ContentStart, rtxTexto.Document.ContentEnd);
				rango.Save(fileStream, DataFormats.Rtf);
			}
		}

		private void rtxTexto_SelectionChanged(object sender, RoutedEventArgs e)
		{
			object temp = rtxTexto.Selection.GetPropertyValue(Inline.FontWeightProperty);
			btnN.IsEnabled = (bool)((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)));

			temp = rtxTexto.Selection.GetPropertyValue(Inline.FontStyleProperty);
			btnI.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

			temp = rtxTexto.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			btnS.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

			temp = rtxTexto.Selection.GetPropertyValue(Inline.FontFamilyProperty);
			cmbFuentes.SelectedItem = temp;

			temp = rtxTexto.Selection.GetPropertyValue(Inline.FontSizeProperty);
			cmbTamanos.Text = temp.ToString();
		}

		private void mitGuardarComo_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
			if (dlg.ShowDialog() == true)
			{
				FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
				TextRange rango = new TextRange(rtxTexto.Document.ContentStart, rtxTexto.Document.ContentEnd);
				rango.Save(fileStream, DataFormats.Rtf);
			}
		}
	}
}
