using System;
using System.Collections.Generic;
					
public class Program
{
	public static void Main()
	{
		Tables TestTables=new Tables();
		
		//Test verileri Random nesnesinin çıkardığı sıkıntılar sebebiyle elle girildi
		TestTables.AddTable(new Table(200,true,true,true));		
		TestTables.AddTable(new Table(200,true,true,true));
		TestTables.AddTable(new Table(2000,false,false,true));
		TestTables.AddTable(new Table(666,false,false,false));
		TestTables.AddTable(new Table(200,true,true,true));
		TestTables.AddTable(new Table(3010,true,false,true));
		TestTables.AddTable(new Table(3010,true,false,true));
		TestTables.AddTable(new Table(3010,true,false,true));
		TestTables.AddTable(new Table(5000,true,false,false));
		TestTables.AddTable(new Table(250,false,false,true));
		TestTables.AddTable(new Table(2000,false,false,true));
		TestTables.AddTable(new Table(250,false,false,true));
		TestTables.AddTable(new Table(250,false,false,true));
		TestTables.AddTable(new Table(700,false,false,false));
		TestTables.AddTable(new Table(700,false,true,false));
		TestTables.AddTable(new Table(700,false,false,false));
		TestTables.AddTable(new Table(600,true,false,true));
		TestTables.AddTable(new Table(600,true,false,false));
		TestTables.AddTable(new Table(976,true,false,true));
		
		TestTables.Show();
		
		List<Object> Found=TestTables.Filter();
		
		//Bulunan masaların listelenmesi
		if(Found.Count!=0){
			Console.WriteLine("\nList of tables found:\n------------------------------------");
			foreach(Object o in Found){
				Console.WriteLine((Table)(o));
			}
		}else{
				Console.WriteLine("No Tables Found.");
		}
	}
}

public class Table{
	static int ID=0;
	int TableID,BetAmount,HiddenValue;
	bool IsFast,IsOneVsOne,IsRevenge;
	
	public Table(int BetAmount, bool IsFast, bool IsOneVsOne, bool IsRevenge){
		this.BetAmount=BetAmount;
		this.IsFast=IsFast;
		this.IsOneVsOne=IsOneVsOne;
		this.IsRevenge=IsRevenge;
		HiddenValue=CalcHiddenValue();
		TableID=ID;
		ID++;
	}
	
	//Masa değişkenlerinin tamsayı olarak kodlanması
	private int CalcHiddenValue(){
		int temp=0;
		if(IsFast){
			temp+=1;
		}
		if(IsOneVsOne){
			temp+=2;
		}
		if(IsRevenge){
			temp+=4;
		}
		temp+=BetAmount*10;
		return temp;
	}
	
	//Kodlarına göre masaları karşılaştıran metod
	public bool IsEqual(int value){
		return HiddenValue==value;
	}
	
	//Masanın yazdırılması için ToString
	public override string ToString(){
		string temp="";
		temp+="Table ID: "+TableID+"\n";
		temp+="Bet Ammount: "+BetAmount+"$\n";
		if(IsFast){
			temp+="Fast Game: Yes\n";
		}else{
			temp+="Fast Game: No\n";
		}
		
		if(IsOneVsOne){
			temp+="One versus One Game: Yes\n";
		}else{
			temp+="One versus One Game: No\n";
		}
		
		if(IsRevenge){
			temp+="Revenge Game: Yes\n";
		}else{
			temp+="Revenge Game: No\n";
		}
		return temp;
	}
}

public class Tables{
	List<Object> TableList=new List<Object>();
	
	public void AddTable(Table NewTable){
		TableList.Add(NewTable);
	}
	
	//Masalardan kullanıcıdan istenen bilgilere uygun olanları döndüren metod
	public List<Object> Filter(){
		int HiddenValue=FilterQuery();
		
		//Predicate ile arama
		return (TableList.FindAll(delegate(Object o){
			return ((Table)(o)).IsEqual(HiddenValue);
		}));
	}
	
	//Kullanıcıdan veri girişi alıp aranan masanın kodunu oluşturan metod
	public int FilterQuery(){
		int BetAmount;
		string IsFast,IsOneVsOne,IsRevenge;
		int temp=0;
		do{
		Console.Write("Specify Bet Amount(200-5000$): ");
		BetAmount=int.Parse(Console.ReadLine());
		if(!(BetAmount<=5000&&BetAmount>=200)){ //Sadece aralığa bakıldı, sayı mı değil mi bakılmadı
			Console.Write("\nOut of Bounds. \n");
		}else{
			temp+=BetAmount*10;
		}
		}while(!(BetAmount<=5000&&BetAmount>=200));
		do{
		Console.Write("Looking for Fast Games?(y/n): ");
		IsFast=Console.ReadLine().ToLower();
		if(!(IsFast.Equals("y")||IsFast.Equals("n"))){
			Console.Write("\nFauly Input. \n");
		}else if(IsFast.Equals("y")){		
			temp+=1;
		}
		}while(!(IsFast.Equals("y")||IsFast.Equals("n")));
		do{
		Console.Write("Looking for One vs. One Games?(y/n): ");
		IsOneVsOne=Console.ReadLine().ToLower();
		if(!(IsOneVsOne.Equals("y")||IsOneVsOne.Equals("n"))){
			Console.Write("\nFauly Input. \n");
		}else if(IsOneVsOne.Equals("y")){		
			temp+=2;
		}
		}while(!(IsOneVsOne.Equals("y")||IsOneVsOne.Equals("n")));
		do{
		Console.Write("Looking for Revenge Games?(y/n): ");
		IsRevenge=Console.ReadLine().ToLower();
		if(!(IsRevenge.Equals("y")||IsRevenge.Equals("n"))){
			Console.Write("\nFauly Input. \n");
		}else if(IsRevenge.Equals("y")){		
			temp+=4;
		}
		}while(!(IsRevenge.Equals("y")||IsRevenge.Equals("n")));
		
		return temp;
	}
	
	//Tüm masaları yazdıran metod
	public void Show(){
		foreach(Object o in TableList){
			Console.WriteLine((Table)(o));
		}
	}	
}



			
