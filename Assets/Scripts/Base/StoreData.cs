
public enum Day {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

public class StoreData 
{
    public string storeName = "Unknown Store";
    public string ownerName = "Unknown Owner";

    public int money = 0;

    public Day day = 0;
    public int week = 1;
    public int month = 1;

    public string GetCurrentDayString() {
        string dayString = "";
        switch (day) {
            case Day.Monday:
                dayString = "Monday";
                break;
            case Day.Tuesday:
                dayString = "Tuesday";
                break;
            case Day.Wednesday:
                dayString = "Wednesday";
                break;
            case Day.Thursday:
                dayString = "Thursday";
                break;
            case Day.Friday:
                dayString = "Friday";
                break;
            case Day.Saturday:
                dayString = "Saturday";
                break;
            case Day.Sunday:
                dayString = "Sunday";
                break;
        }

        return dayString;
    }
    public string GetCurrentDayShortString() {
        string dayString = "";
        switch (day) {
            case Day.Monday:
                dayString = "Mon";
                break;
            case Day.Tuesday:
                dayString = "Tues";
                break;
            case Day.Wednesday:
                dayString = "Wed";
                break;
            case Day.Thursday:
                dayString = "Thur";
                break;
            case Day.Friday:
                dayString = "Fri";
                break;
            case Day.Saturday:
                dayString = "Sat";
                break;
            case Day.Sunday:
                dayString = "Sun";
                break;
        }

        return dayString;
    }
}
