﻿namespace IntergalacticControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Forms the base of stack controllers
    /// </summary>
    public class StackControllerBase : UserControl
    {
        /// <summary>
        /// Represents the normal opacity for buttons.
        /// </summary>
        private const double NormalOpacity = 0.2;

        /// <summary>
        /// Represents the hovered opacity for buttons.
        /// </summary>
        private const double HoveredOpacity = 1.0;
    
        /// <summary>
        /// Fade in/out animations
        /// </summary>
        private DoubleAnimation fadeIn, fadeOut;

        /// <summary>
        /// The primary stack panel to use
        /// </summary>
        private StackPanel mainStackPanel = null;

        /// <summary>
        /// Initializes a new instance of the StackControllerBase class
        /// </summary>
        public StackControllerBase()
        {
            if (this.FadeControllerOnHover)
            {
                this.Opacity = StackControllerBase.NormalOpacity;
            }
            else
            {
                this.Opacity = 1.0;
            }

            this.fadeIn = new DoubleAnimation(StackControllerBase.HoveredOpacity, TimeSpan.FromSeconds(0.3));
            this.fadeIn.AccelerationRatio = 0.3;
            this.fadeIn.DecelerationRatio = 0.3;
            this.fadeOut = new DoubleAnimation(StackControllerBase.NormalOpacity, TimeSpan.FromSeconds(0.3));
            this.fadeOut.AccelerationRatio = 0.3;
            this.fadeOut.DecelerationRatio = 0.3;

            this.MouseLeave += this.HidePopupView;
        }

        /// <summary>
        /// Gets or sets the main stack panel
        /// </summary>
        protected StackPanel MainStackPanel
        {
            get { return this.mainStackPanel; }
            set { this.mainStackPanel = value; }
        }

        /// <summary>
        /// Gets a value indicating whether to hide the popup view when the mouse leaves the stack controller
        /// </summary>
        protected virtual bool HidePopupOnMouseLeave
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether to fade the stack controller when the mouse hovers.
        /// </summary>
        protected virtual bool FadeControllerOnHover
        {
            get { return true; }
        }

        /// <summary>
        /// Adds a stack button to this controls
        /// </summary>
        /// <param name="button">The button</param>
        public void AddButton(StackButtonBase button)
        {
            button.IsVertical = this.mainStackPanel.Orientation == Orientation.Vertical;
            this.mainStackPanel.Children.Add(button);
            button.MouseEnter += new MouseEventHandler(this.PanelButton_MouseEnter);
        }

        /// <summary>
        /// Removes a stack button to this controls
        /// </summary>
        /// <param name="button">The button</param>
        public void RemoveButton(StackButtonBase button)
        {
            this.mainStackPanel.Children.Remove(button);
        }

        /// <summary>
        /// MouseEnter function to add animation
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.FadeControllerOnHover)
            {
                this.BeginAnimation(UserControl.OpacityProperty, this.fadeIn);
            }
        }

        /// <summary>
        /// MouseLeave function to add animation
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.FadeControllerOnHover)
            {
                this.BeginAnimation(UserControl.OpacityProperty, this.fadeOut);
            }
        }

        /// <summary>
        /// MouseEnter function for the stack buttons added to this control
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void PanelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackButtonBase source = sender as StackButtonBase;
            UIManager.CurrentUIManager.ViewPopup(source);
        }

        /// <summary>
        /// Closes the popup view when the mouse exits the tab button
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void HidePopupView(object sender, MouseEventArgs e)
        {
            if (this.HidePopupOnMouseLeave)
            {
                UIManager.CurrentUIManager.HideCurrentPopup();
            }
        }
    }
}
