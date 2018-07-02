class TextFormatter
{
    public getFormattedLines(data: string): string[]
    {
        var words = data.split(" ");
        var line = "";
        var lines: string[] = [];
        for (var i = 0; i < words.length; i++)
        {
            //if ()
            //    line.length = 24;
            //else
            if (line.length > 23 || words[i] == "\\n")
            {
                words[i] = null
                lines.push(line);
                line = "";
            }
            if (words[i])
                line = `${line} ${words[i]}`;
        }
        if (line.length > 0)
            lines.push(line);
        return lines;
    }
}