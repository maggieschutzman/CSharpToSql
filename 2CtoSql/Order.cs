using System;
using System.Collections.Generic;
using System.Text;

namespace _2CtoSql {
    class Order {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int CustomerId { get; set; }

        public Order(int id, DateTime date, string note, int customerid) {
            this.Id = id;
            this.Date = date;
            this.Note = note;
            this.CustomerId = customerid;
        }
    }
}
