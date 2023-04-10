class MatrixProcessor {

    private readonly String fileName;
    private List<String> matrix = new List<string>();

    public MatrixProcessor(string fileName)
    {
        this.fileName = fileName;
    }

    private String matrixLongestSecuence = "";

    void ReadMatrixFromFile() {
        List<String> fileContent = new List<string>();
        using(StreamReader file = new StreamReader(fileName)) {
            int counter = 0;
            string ln;

            while ((ln = file.ReadLine()) != null) {
                fileContent.Add(ln.Replace(",", ""));
                counter++;
            }
            file.Close();
        }       
        this.matrix = fileContent;
    }

    public String getLongestSecuence() {
        ReadMatrixFromFile();

        findLongestHorizontal();
        
        findLongesVertical();

        ProcessDiagonals(true);

        ProcessDiagonals(false);
        
        return this.matrixLongestSecuence;
    }

    private void findLongestHorizontal()
    {
        for (int i = 0; i < this.matrix.Count; i++)
        {
            SaveIfLongest(getLongest(matrix[i]));
        }
    }

    private void findLongesVertical()
    {
        string column = "";
        for (int i = 0; i < matrix.Count; i++)
        {
            column = getColumn(i);
            SaveIfLongest(getLongest(column));
            
        }
    }

    /// <summary>
    /// Iterates over diagonals. When firstColumn > lastColumn, iterates from top/left to bottom/right.
    /// When increment = -1, iterates from top/right to bottom/left.
    /// </summary>
    private void ProcessDiagonals(bool Left2Right)
    {
        string diagonal = "";
        int firstRow = 0;
        int lastRow = matrix.Count;
        int firstColumn = 0; 
        int lastColumn = matrix.Count;
    
        //iterates over first row
        for (int i = firstColumn; i < lastColumn; i++)
        {
            diagonal = getDiagonal(i, firstRow, Left2Right );
            SaveIfLongest(getLongest(diagonal));
        }

        //iterates over first column, begining from de second row (first was processed in last step)
        for (int i = firstRow + 1; i < lastRow; i++)
        {
            diagonal = getDiagonal(Left2Right ? firstColumn : lastColumn - 1, i, Left2Right);
            SaveIfLongest(getLongest(diagonal));
            
        }
    }

    private string getColumn(int colnumber)
    {
        string column = "";
        for (int i = 0; i < matrix.Count; i++)
        {
            column += matrix[i][colnumber];
        }
        return column;
    }

    private string getDiagonal(int colnumber, int rownumber, bool Left2Right)
    {
        string column = "";
        int increment = Left2Right ? 1 : -1;

        do
        {
            do
            {
                column += matrix[rownumber][colnumber];
                colnumber = colnumber + increment;
                rownumber++;
            } while (colnumber < matrix.Count && rownumber < matrix.Count && colnumber >= 0);
        } while (rownumber < matrix.Count && colnumber < matrix.Count && colnumber >= 0);

        return column;
    }

    private void SaveIfLongest(string secuence)
    {
        if (secuence.Length > this.matrixLongestSecuence.Length)
            this.matrixLongestSecuence = secuence;
    }

    private string getLongest(string secuence) {
        int maxStartIndex = 0;
        int maxLength = 1;
        int startIndex = 0;
        int length = 1;
                
        for (int i = 1; i < secuence.Length; i++)
        {
            if (secuence[i] == secuence[i - 1])
            {
                length++;
            }
            else
            {
                if (length > maxLength)
                {
                    maxLength = length;
                    maxStartIndex = startIndex;
                }
                startIndex = i;
                length = 1;
            }
        }

        if (length > maxLength)
        {
            maxLength = length;
            maxStartIndex = startIndex;
        }
        return secuence.Substring(maxStartIndex, maxLength);
    }

}