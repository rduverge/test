using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorCobrar.Shared;

public class AccountingEntry
{
     
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountingEntryId { get; set; }

    
    public string? Description { get; set; }




     
  //   [ForeignKey("CustomerId")]
  
    public int? CustomerId { get; set; }
    public virtual  Customer? Customer { get; set; }


    
    
    public int Account { get; set; }


    public MovementTypes? MovementType { get; set; } 

    
    public DateTime AccountEntryDate { get; set; }

   
    public decimal AccountEntryAmount { get; set;}

    public States? State { get; set; } 

    

}

