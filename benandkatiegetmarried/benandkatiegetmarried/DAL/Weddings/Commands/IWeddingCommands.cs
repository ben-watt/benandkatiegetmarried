using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;

namespace benandkatiegetmarried.DAL.Weddings.Commands
{
    public interface IWeddingCommands
    {
        void Create(Models.Wedding wedding);
        void UpdateWedding(Wedding wedding);
    }
}
