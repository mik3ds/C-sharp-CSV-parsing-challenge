using System;
using System.IO;
using System.Collections.Generic;

class MainClass {
  public static void Main (string[] args) {

    //create timer to track how long function takes to run
    var watch = new System.Diagnostics.Stopwatch();
    watch.Start();

    //open input csv file
    using (var reader = new StreamReader("in.csv")) {

      //pass over opening line of csv that contains coloumn names
      var headerLine = reader.ReadLine();

      //variable to keep track of how many rows have been processed
      var numberOfItemsProcessed = 0;

      //loop through input csv file
      while(!reader.EndOfStream){

        //split values by comma
        var values = reader.ReadLine().Split(",");

        //check if output file for store already exists and create one if it doesnt
        if(!File.Exists($"{values[0]}.csv")){
          var newFile = File.Create($"{values[0]}.csv");
          newFile.Close();
        }

        //open output file
        using (var writer = new StreamWriter($"{values[0]}.csv", true)) {
          //set discount value
          var discount = -0.05;
          //write SKU and discounted price to csv
          writer.WriteLine($"{values[1]},{Convert.ToDouble(values[2]) - (Convert.ToDouble(values[2]) * discount)}");
          writer.Close();
        }
        numberOfItemsProcessed += 1;
      }

      watch.Stop();

      Console.WriteLine($"{numberOfItemsProcessed} items processed in {watch.ElapsedMilliseconds}ms");
      }
  }
}