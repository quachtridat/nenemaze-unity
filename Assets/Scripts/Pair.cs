public class Pair<T1, T2> {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }

    public Pair() { }

    public Pair(Pair<T1, T2> p) {
        Item1 = p.Item1;
        Item2 = p.Item2;
    }

    public Pair(T1 item1, T2 item2) {
        Item1 = item1;
        Item2 = item2;
    }

    public static Pair<T1, T2> Create(T1 item1, T2 item2) {
        return new Pair<T1, T2> { Item1 = item1, Item2 = item2 };
    }
}