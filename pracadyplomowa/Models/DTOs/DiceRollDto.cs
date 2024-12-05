public class DiceRollDto
{
    public int d20 { get; set; }
    public int d12 { get; set; }
    public int d10 { get; set; }
    public int d8 { get; set; }
    public int d6 { get; set; }
    public int d4 { get; set; }
    public int d100 { get; set; }
    public int flat { get; set; }

    public DiceRollDto()
    {
    }

    public DiceRollDto(int[] arr)
    {
        if (arr == null || arr.Length != 8)
        {
            throw new ArgumentException();
        }

        this.d20 = arr[0];
        this.d12 = arr[1];
        this.d10 = arr[2];
        this.d8 = arr[3];
        this.d6 = arr[4];
        this.d4 = arr[5];
        this.d100 = arr[6];
        this.flat = arr[7];
    }
}