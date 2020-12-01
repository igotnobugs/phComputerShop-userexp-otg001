
public enum Day {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

[System.Serializable]
public class StoreData 
{
    public string storeName = "Unknown Store";
    public string ownerName = "Unknown Owner";

    public int earnings = 0;
    public int money = 0;
    public int moneyNeeded = 0;

    public Day day = 0; // 7 days per week (0 -> 6)
    public int week = 1; //4 weeks per month
    public int month = 1; //12 months

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
    public string GetWeekMonth() {
        return week.ToString() + " / " + month.ToString();
    }

    public void AdvanceDay() {
        earnings = 0;

        if ((int)day >= 6) {
            day = 0;
            week++;
        }
        else {day++;}

        if (week > 4) {
            week = 1;
            month++;
        }

        if (month > 12) {
            month = 1;
        }
    }

    public void AddMoney(int amount) {
        if (amount >= 0) {
            money += amount;
            earnings += amount;
        }     
    }

    public void AddMoneyNoEarnings(int amount) {
        money += amount;
    }

    public void DeductMoney(int amount) {
        if (amount < 0) {
            money -= amount;
        }
    }
}
