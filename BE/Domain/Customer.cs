
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; 
namespace CuentasPorCobrar.Shared;

public class Customer
{
    public Customer()
    {

        AccountingEntries=new HashSet<AccountingEntry>();
        Transactions=new HashSet<Transaction>(); 

    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    
   
    public string? Name { get; set; }

    
  
    public string? Identification { get; set; }


    [Column(TypeName = "money")]
    public decimal CreditLimit { get; set; }


    public States? State { get; set; }  

    public virtual ICollection<AccountingEntry> AccountingEntries{get; set;} 
    public virtual ICollection<Transaction> Transactions { get; set; }



}

