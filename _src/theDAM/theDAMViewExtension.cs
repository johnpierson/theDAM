﻿using System;
using System.Windows;
using System.Windows.Controls;
using Dynamo.Controls;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;
using SampleViewExtension;

namespace theDAM
{
    /// <summary>
    /// The View Extension framework for Dynamo allows you to extend
    /// the Dynamo UI by registering custom MenuItems. A ViewExtension has 
    /// two components, an assembly containing a class that implements 
    /// IViewExtension, and an ViewExtensionDefintion xml file used to 
    /// instruct Dynamo where to find the class containing the
    /// IViewExtension implementation. The ViewExtensionDefinition xml file must
    /// be located in your [dynamo]\viewExtensions folder.
    /// 
    /// This sample demonstrates an IViewExtension implementation which 
    /// shows a modeless window when its MenuItem is clicked. 
    /// The Window created tracks the number of nodes in the current workspace, 
    /// by handling the workspace's NodeAdded and NodeRemoved events.
    /// </summary>
    public class theDAM : IViewExtension
    {
        private MenuItem _theDAMMenuItem;

        public void Dispose()
        {
        }
        public static DynamoView view;
        public void Startup(ViewStartupParams p)
        {
        }

        public void Loaded(ViewLoadedParams p)
        {

            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            view = p.DynamoWindow as DynamoView;

            _theDAMMenuItem = new MenuItem { Header = "theDAM" };

            //change the menu font color and add it to the dynamo ribbon
            p.dynamoMenu.Items.Add(_theDAMMenuItem);
        }

        public void Shutdown()
        {
        }

        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public string Name
        {
            get
            {
                return "theDAM View Extension";
            }
        }
        public static DynamoViewModel dynView
        {
            get { return view.DataContext as DynamoViewModel; }
        }
    }
}