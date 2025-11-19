namespace Save_Load_WebAPI.Model
{
    public class Player
    {
        public required string Id { get; set; }
        public int Vida { get; set; }
        public int QuantidadeItens { get; set; }
        public float PosicaoX { get; set; }
        public float PosicaoY { get; set; }
        public float PosicaoZ { get; set; }
    }
}
