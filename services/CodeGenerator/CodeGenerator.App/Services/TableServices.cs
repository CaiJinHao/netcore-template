using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using CodeGenerator.App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.App.Extensions;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace CodeGenerator.App.Services
{
    public class TableServices
    {
        private TableRepository tableRepository { get; set; }
        public TableServices()
        {
            tableRepository= RepositoryFactory.CreateTableRepository();
        }

        public async Task<IEnumerable<TablesModel>> GetModelsAsync() {
           return await tableRepository.GetModelsAsync();
        }
    }
}
