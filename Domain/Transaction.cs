
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CuentasPorCobrar.Shared;

public class Transaction 
{

   
   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    
    public MovementTypes? MovementType { get; set; }

    public int? DocumentId { get; set; }
    public virtual Document? Document { get; set; }




    public Guid? DocumentNumber { get; set; }

    
    public DateTime TransactionDate { get; set; }


    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

   
    
    public decimal Amount { get; set; }

    
}