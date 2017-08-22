using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using PowerShell = System.Management.Automation.PowerShell;
using System.Diagnostics;
using System.IO;

namespace PurchaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
        //Data Instantiation
            char inputNumber = '0';
            Program PurchaseProg = new Program();
            //Create a string for holding the path to the current data file.
            string dataPath = String.Empty;
            //Create a string for holding the path to the purchase Order
            string PurchaseOrderFilePath = String.Empty;

        //Process

            PurchaseProg.writeMenu();
            
            //Store the input
            inputNumber = Console.ReadKey().KeyChar;
            Console.WriteLine();
            
            while (inputNumber != '4')
            {
                switch (inputNumber)
                {
                    case '1':
                        PurchaseProg.createNewFile(ref dataPath);
                        break;
                    case '2':
                        //Check for a data file first.  If it does not exist, break out. 
                        if (String.IsNullOrEmpty(dataPath)){
                            Console.WriteLine("No data file created yet.  Please create a data file first.");
                        }
                        else {
                            PurchaseProg.createPurchaseOrder(ref dataPath);
                        }
                        break;
                    case '3':
                        //Check for existence of purchase order. If it does not exist, break out.
                        if (String.IsNullOrEmpty(dataPath))
                        {
                            Console.WriteLine("No purchase order created yet.  Please create a purchase order first.");
                        }
                        else
                        {
                            PurchaseProg.sendEmail(ref PurchaseOrderFilePath);
                        }
                        break;
                    case '4':
                        break;
                    default:
                        Console.WriteLine("You did not enter a correct option.  Please try again.");
                        break;
                }
                if(inputNumber != 4){
                    Console.WriteLine("Please select a new option. ");
                    inputNumber = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                }
            }

            return;
                      
        }

        public void createNewFile(ref string dataPath){

            //Variables
            String NewFileName;
            int NumRecords;
            char decision;
            string dataLines;
            
            //I NEED TO BUNDLE THIS SCRIPT INTO THIS SOLUTION FILE.  
            string scriptPath = @"C:\Scripts\DataScript.ps1";
            string script = File.ReadAllText(scriptPath);

            PowerShell powershell = PowerShell.Create();
            powershell.AddScript(script);
                        
            Console.WriteLine("Please enter a new file name");
            NewFileName = Console.ReadLine();

            //Set the new path to the data
            dataPath = @"C:\Scripts\" + NewFileName;

            //Prompt for the amount of data lines to be made.  
            Console.WriteLine("Please enter an amount of records to be made");

            //Store the amount to be input as a parameter to the script.  
            NumRecords = Convert.ToInt32(Console.ReadLine());
            
            powershell.AddParameter(NewFileName, NumRecords);
            powershell.Invoke();
            Console.WriteLine("Would you like to see the data? Input Y or N");
            decision = Console.ReadKey().KeyChar;

            if (Char.ToUpper(decision) == 'Y')
            {
                dataLines = String.Empty;
                dataLines = File.ReadAllText(@"C:\Users\jessem\Purchase_Tables\TestFile.csv");
                Console.WriteLine(dataLines);
                writeMenu();
            }
            
        }

        public void sendEmail(ref string PurchaseOrderFilePath){

        }

        public void createPurchaseOrder(ref String dataPath){

            //Variables
            FileStream DataFile = new FileStream(dataPath, FileMode.OpenOrCreate, FileAccess.Read);

            //I NEED TO MAKE THIS FILE PATH UNIVERSAL.  
            //Create the purchase order csv file to allow for writing to it as the data file is processed.
            FileStream PurchaseOrder = new FileStream(@"C:\users\jessem\Purchase_Tables\PurchaseOrder.csv", FileMode.OpenOrCreate, FileAccess.Write);

            //Process

     
               
        }

        public void writeMenu()
        {
            Console.WriteLine("Automation of Purchase Orders.");
            Console.WriteLine("Please enter the corresponding number for the listed options:");
            Console.WriteLine("1.  Create a new sample data file");
            Console.WriteLine("2.  Create a purchase order for the next day");
            Console.WriteLine("3.  Send email of purchase order");
            Console.WriteLine("4.  Exit the Application");
        }
    }
}


