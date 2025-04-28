/**
 * 
 */


/**
 * @author Salih
 *
 */
[System.Serializable]
public class EnforaIO {
	
	private string foid, input1Name, input2Name, input3Name;

    public string Input1Name
    {
      get { return input1Name; }
      set { input1Name = value; }
    }

    public string Input2Name
    {
        get { return input2Name; }
        set { input2Name = value; }
    }

    public string Input3Name
    {
        get { return input3Name; }
        set { input3Name = value; }
    }

    public string Foid
    {
      get { return foid; }
      set { foid = value; }
    }

	/**
	 * @param foid
	 */
    public EnforaIO(string foid)
    {
		this.foid = foid;
	}

    public EnforaIO()
    {
        this.foid = foid;
    }

	/**
	 * @return the foid
	 */
    public string getFoid()
    {
		return Foid;
	}

	/**
	 * @param foid the foid to set
	 */
    public void setFoid(string foid)
    {
		this.Foid = foid;
	}

	/**
	 * @return the input1
	 */
    public string getInput1Name()
    {
		return Input1Name;
	}

	/**
	 * @param input1 the input1 to set
	 */
    public void setInput1Name(string input1)
    {
		this.Input1Name = input1;
	}

	/**
	 * @return the input2
	 */
    public string getInput2Name()
    {
		return input2Name;
	}

	/**
	 * @param input2 the input2 to set
	 */
    public void setInput2Name(string input2)
    {
		this.input2Name = input2;
	}

	/**
	 * @return the input3
	 */
    public string getInput3Name()
    {
		return Input3Name;
	}

	/**
	 * @param input3 the input3 to set
	 */
    public void setInput3Name(string input3)
    {
		this.Input3Name = input3;
	}
}