﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Document.Meta;

namespace LuaSTGEditorSharp.Windows.Input
{
    /// <summary>
    /// ImageInput.xaml 的交互逻辑
    /// </summary>
    public partial class SEInput : InputWindow
    {
        public ObservableCollection<MetaModel> seInfo;
        public ObservableCollection<MetaModel> SEInfo { get => seInfo; }

        public ObservableCollection<MetaModel> seInfoSys;
        public ObservableCollection<MetaModel> SEInfoSys { get => seInfoSys; }

        public override string Result
        {
            get => result;
            set
            {
                result = value;
                RaisePropertyChanged("Result");
            }
        }

        public SEInput(string s, AttrItem item)
        {
            seInfo = item.Parent.parentWorkSpace.Meta.aggregatableMetas[(int)MetaType.SELoad].GetAllSimpleWithDifficulty();

            AddInternalMetas();

            InitializeComponent();

            Result = s;
            codeText.Text = Result;
        }

        private void AddInternalMetas()
        {
            seInfoSys = new ObservableCollection<MetaModel>(NodesConfig.SysSE);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void InputWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(codeText);
        }

        private void BoxSEData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MetaModel m = (BoxSEData.SelectedItem as MetaModel);
            if (!string.IsNullOrEmpty(m?.Result)) Result = m?.Result;
            try
            {
                mediaPlayer.Source = new Uri(m?.ExInfo1);
                mediaPlayer.Play();
            }
            catch (Exception exc){ MessageBox.Show(exc.ToString()); }
            codeText.Focus();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                DialogResult = true;
                this.Close();
            }
        }
    }
}
