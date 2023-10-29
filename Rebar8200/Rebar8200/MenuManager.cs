using System;

public class MenuManager
{
    private List<Shake> menu = new List<Shake>();

    public MenuManager()
    {
        // Define an initial menu with sample shakes
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        // Add sample shakes to the menu
        menu.Add(new Shake("Classic Strawberry", "Delicious strawberry flavor", 5.99, 4.99, 3.99, Shake.ShakeSize.NotSelectedYet));
        menu.Add(new Shake("Chocolate Bliss", "Rich and creamy chocolate shake", 6.49, 5.49, 4.49, Shake.ShakeSize.NotSelectedYet));
        menu.Add(new Shake("Tropical Paradise", "Exotic blend of tropical fruits", 7.99, 6.99, 5.99, Shake.ShakeSize.NotSelectedYet));
        // You can add more sample shakes here
    }

    // Method to add a shake to the menu
    public void AddShakeToMenu(Shake shake)
    {
        menu.Add(shake);
    }

    // Method to retrieve the entire menu
    public List<Shake> GetMenu()
    {
        return menu;
    }
}
