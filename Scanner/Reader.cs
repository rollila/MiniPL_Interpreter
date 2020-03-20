namespace MiniPL
{
    interface Reader
    {
        void init();
        Character peek();
        Character next();
        Character previous();

    }
}