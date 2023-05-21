using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SchoolFeeCollectionSoftware
{
    // Class to represent a student
    class Student
    {
        public string AdmissionNumber { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public double FeeBalance { get; set; }
        public List<double> FeePayments { get; set; }

        public Student(string admissionNumber, string name, int studentClass)
        {
            AdmissionNumber = admissionNumber;
            Name = name;
            Class = studentClass;
            FeeBalance = 0;
            FeePayments = new List<double>();
        }
    }

    // Class to manage fee collection
    class FeeManager
    {
        private List<Student> students;
        private int receiptNumber;

        public FeeManager()
        {
            students = new List<Student>();
            receiptNumber = 1;
        }

        // Method to add a new student
        public void AddStudent(string admissionNumber, string name, int studentClass)
        {
            Student student = new Student(admissionNumber, name, studentClass);
            students.Add(student);
            MessageBox.Show("Student added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Method to remove a student
        public void RemoveStudent(string admissionNumber)
        {
            Student student = students.Find(s => s.AdmissionNumber == admissionNumber);

            if (student != null)
            {
                students.Remove(student);
                MessageBox.Show("Student removed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to search for a student
        public void SearchStudent(string admissionNumber)
        {
            Student student = students.Find(s => s.AdmissionNumber == admissionNumber);

            if (student != null)
            {
                MessageBox.Show($"Admission Number: {student.AdmissionNumber}\nName: {student.Name}\nClass: {student.Class}\nFee Balance: {student.FeeBalance}\nFee Payments:\n{string.Join("\n", student.FeePayments)}", "Student Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to record fee payment
        public void RecordPayment(string admissionNumber, double amount)
        {
            Student student = students.Find(s => s.AdmissionNumber == admissionNumber);

            if (student != null)
            {
                student.FeeBalance -= amount;
                student.FeePayments.Add(amount);
                MessageBox.Show($"Payment recorded successfully.\nReceipt Number: {receiptNumber}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                receiptNumber++;
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Main program
    public partial class MainWindow : Window
    {
        private FeeManager feeManager;

        public MainWindow()
        {
            InitializeComponent();
            feeManager = new FeeManager();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string admissionNumber = AdmissionNumberTextBox.Text;
            string name = NameTextBox.Text;
            int studentClass = Convert.ToInt32(ClassTextBox.Text);
            feeManager.AddStudent(admissionNumber, name, studentClass);
        }

        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string admissionNumber = AdmissionNumberTextBox.Text;
            feeManager.RemoveStudent(admissionNumber);
        }

        private void SearchStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string admissionNumber = AdmissionNumberTextBox.Text;
            feeManager.SearchStudent(admissionNumber);
        }

        private void RecordPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            string admissionNumber = AdmissionNumberTextBox.Text;
            double amount = Convert.ToDouble(PaymentAmountTextBox.Text);
            feeManager.RecordPayment(admissionNumber, amount);
        }
    }
}
