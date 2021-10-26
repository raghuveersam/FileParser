using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser
{
    public partial class ResultDisplayer : Form
    {
        public ResultDisplayer()
        {
            InitializeComponent();

            WordBuilder wordBuilder = new WordBuilder();
            wordBuilder.ComputeWords();

            // Updating the form text box with the computed result
            textBox1.Text = wordBuilder.FirstLargestWord.ToString();
            textBox2.Text = wordBuilder.SecondLargestWord.ToString();
            textBox3.Text = wordBuilder.MaximumCount.ToString();
        }

    }
}
