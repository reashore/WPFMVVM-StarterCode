﻿using System;
using System.ComponentModel;
using System.Windows;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public partial class CustomerEditView
    {
        private readonly ICustomersRepository _repository = new CustomersRepository();
        private Customer _customer;

        public CustomerEditView()
        {
            InitializeComponent();
        }

        public Guid CustomerId
        {
            get { return (Guid)GetValue(CustomerIdProperty); }
            set { SetValue(CustomerIdProperty, value); }
        }

        public static readonly DependencyProperty CustomerIdProperty =
            DependencyProperty.Register("CustomerId", typeof(Guid), typeof(CustomerEditView), new PropertyMetadata(Guid.Empty));

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            _customer = await _repository.GetCustomerAsync(CustomerId);

            if (_customer == null)
            {
                return;
            }

            FirstNameTextBox.Text = _customer.FirstName;
            LastNameTextBox.Text = _customer.LastName;
            PhoneTextBox.Text = _customer.Phone;
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            // TODO: Validate input... call business rules... etc...
            _customer.FirstName = FirstNameTextBox.Text;
            _customer.LastName = LastNameTextBox.Text;
            _customer.Phone = PhoneTextBox.Text;
            await _repository.UpdateCustomerAsync(_customer);
        }
    }
}
