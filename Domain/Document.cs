
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorCobrar.Shared;


public class Document
{
    public Document()
    {
        Transactions=new HashSet<Transaction>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocumentId { get; set; }


   
  
    public string? Description { get; set; }


    
   
    public int LedgerAccount { get; set; }


    public States? State { get; set; }



    public virtual ICollection<Transaction> Transactions { get; set; }

}

