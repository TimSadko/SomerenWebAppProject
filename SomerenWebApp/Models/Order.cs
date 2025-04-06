namespace SomerenWebApp.Models
{
	public class Order
	{
		private int _id;
		private int _drink_id;
		private int _student_number;
		private decimal _price;

		public Order()
		{
		}

		public Order(int _id, int _drink_id, int _student_number, decimal _price)
		{
			this._id = _id;
			this._drink_id = _drink_id;
			this._student_number = _student_number;
			this._price = _price;
			
		}

		public int Id { get => _id; set => _id = value; }
		public int DrinkID { get => _drink_id; set => _drink_id = value; }
		public int StudentNumber { get => _student_number; set => _student_number = value; }
		public decimal Price { get => _price; set => _price = value; }
		

		public override string? ToString()
		{
			return $"_id: {_id}; _drink_id: {_drink_id}; _student_number: {_student_number}; _price: {_price};";
		}
	}
}
