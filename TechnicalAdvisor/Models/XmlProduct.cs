using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class XmlProduct
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ProductId { get; set; }


        public string TituloDoBloco { get; set; }
        public string InfosDiversas { get; set; }
        public string LinkDaImagem { get; set; }
        public string MaisInfos { get; set; }

        public XmlProduct(string fileName, int productId, string tituloDoBloco, string infosDiversas) : this(fileName, productId)
        {
            TituloDoBloco = tituloDoBloco;
            InfosDiversas = infosDiversas;
        }

        public XmlProduct(string fileName, int productId, string tituloDoBloco, string infosDiversas, string linkDaImagem, string maisInfos) : this(fileName, productId)
        {
            TituloDoBloco = tituloDoBloco;
            InfosDiversas = infosDiversas;
            LinkDaImagem = linkDaImagem;
            MaisInfos = maisInfos;
        }

        public XmlProduct(int id, string fileName, int productId, string tituloDoBloco, string infosDiversas, string linkDaImagem, string maisInfos) : this(id, fileName, productId)
        {
            TituloDoBloco = tituloDoBloco;
            InfosDiversas = infosDiversas;
            LinkDaImagem = linkDaImagem;
            MaisInfos = maisInfos;
        }

        public XmlProduct()
        {
        }

        public XmlProduct(string fileName, int productId)
        {
            FileName = fileName;
            ProductId = productId;
        }

        public XmlProduct(int id, string fileName, int productId)
        {
            Id = id;
            FileName = fileName;
            ProductId = productId;
        }


        //Caso quando eu der um update no DB, e não criar a tabela do XmlProduct, o script pra criar ele no DB é:

//        CREATE TABLE[dbo].[XmlProduct]
//        (

//   [Id] INT            NOT NULL,
//    [FileName] NVARCHAR(MAX) NULL,
//    [ProductId] INT NOT NULL,
//    [TituloDoBloco] NVARCHAR(MAX) NULL,
//    [InfosDiversas] NVARCHAR(MAX) NULL,
//    [LinkDaImagem] NVARCHAR(MAX) NULL,
//    [MaisInfos] NVARCHAR(MAX) NULL,
//    CONSTRAINT[PK_XmlProduct] PRIMARY KEY CLUSTERED([Id] ASC)
//);


      
    }
}
