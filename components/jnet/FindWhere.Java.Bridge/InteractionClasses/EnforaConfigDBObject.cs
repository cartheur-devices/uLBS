using System;
[System.Serializable]
public class EnforaConfigDBObject {
	public static  int UNKNOWN = -1;
	
	private String fmid;
	
	private int		curr__IntDaily,
					curr__IntMotion,
					curr__IntNoMotion,
					curr__IntNoGPS,
					
					req__IntDaily = -1,
					req__IntMotion = -1,	
					req__IntNoMotion = -1,
                    req__IntNoGPS = -1,

                    curr__Input1,
                    curr__Input2,
                    curr__Input3,

                    req__Input1		= -1,
					req__Input2		= -1,
                    req__Input3 = -1;

    public int Curr__Input3
    {
        get { return curr__Input3; }
        set { curr__Input3 = value; }
    }

    public int Curr__Input2
    {
        get { return curr__Input2; }
        set { curr__Input2 = value; }
    }

    public int Curr__Input1
    {
        get { return curr__Input1; }
        set { curr__Input1 = value; }
    }

    public int Req__Input1
    {
        get { return req__Input1; }
        set { req__Input1 = value; }
    }

    public int Req__Input2
    {
        get { return req__Input2; }
        set { req__Input2 = value; }
    }

    public int Req__Input3
    {
        get { return req__Input3; }
        set { req__Input3 = value; }
    }

    public int Req__IntNoGPS
    {
        get { return req__IntNoGPS; }
        set { req__IntNoGPS = value; }
    }

    public int Req__IntNoMotion
    {
        get { return req__IntNoMotion; }
        set { req__IntNoMotion = value; }
    }

    public int Req__IntMotion
    {
        get { return req__IntMotion; }
        set { req__IntMotion = value; }
    }

    public int Req__IntDaily
    {
        get { return req__IntDaily; }
        set { req__IntDaily = value; }
    }

    public int Curr__IntNoGPS
    {
        get { return curr__IntNoGPS; }
        set { curr__IntNoGPS = value; }
    }

    public int Curr__IntNoMotion
    {
        get { return curr__IntNoMotion; }
        set { curr__IntNoMotion = value; }
    }

    public int Curr__IntMotion
    {
        get { return curr__IntMotion; }
        set { curr__IntMotion = value; }
    }

    public int Curr__IntDaily
    {
        get { return curr__IntDaily; }
        set { curr__IntDaily = value; }
    }

	public String getFmid() {
		return fmid;
	}

	public void setFmid(String fmid) {
		this.fmid = fmid;
	}

	public int getCurr__IntDaily() {
		return Curr__IntDaily;
	}

	public void setCurr__IntDaily(int curr__IntDaily) {
		this.Curr__IntDaily = curr__IntDaily;
	}

	public int getCurr__IntMotion() {
		return Curr__IntMotion;
	}

	public void setCurr__IntMotion(int curr__IntMotion) {
		this.Curr__IntMotion = curr__IntMotion;
	}

	public int getCurr__IntNoMotion() {
		return Curr__IntNoMotion;
	}

	public void setCurr__IntNoMotion(int curr__IntNoMotion) {
		this.Curr__IntNoMotion = curr__IntNoMotion;
	}

	public int getCurr__IntNoGPS() {
		return Curr__IntNoGPS;
	}

	public void setCurr__IntNoGPS(int curr__IntNoGPS) {
		this.Curr__IntNoGPS = curr__IntNoGPS;
	}

	public int getReq__IntDaily() {
		return Req__IntDaily;
	}

	public void setReq__IntDaily(int req__IntDaily) {
		this.Req__IntDaily = req__IntDaily;
	}

	public int getReq__IntMotion() {
		return Req__IntMotion;
	}

	public void setReq__IntMotion(int req__IntMotion) {
		this.Req__IntMotion = req__IntMotion;
	}

	public int getReq__IntNoMotion() {
		return Req__IntNoMotion;
	}

	public void setReq__IntNoMotion(int req__IntNoMotion) {
		this.Req__IntNoMotion = req__IntNoMotion;
	}

	public int getReq__IntNoGPS() {
		return Req__IntNoGPS;
	}

	public void setReq__IntNoGPS(int req__IntNoGPS) {
		this.Req__IntNoGPS = req__IntNoGPS;
	}	
}