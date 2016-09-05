class StringReader
{
    private index: number;
    private lines: string[];

    constructor(data: string)
    {
        this.index = 0;
        this.lines = data.split("\r\n")
    }

    public readLine(): string
    {
        if (this.index >= this.lines.length)
            return null;
        var line = this.lines[this.index];
        this.index++;
        return line;
    }
}