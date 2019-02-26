
#region Lists
public class Button() { }
 
list<Button> buttons;
buttons.ForEach(ChangeColor);

void ChangeColor(Button b) { }

ForEach(Action<Button> b);

buttons.ForEach(b=>b.color=Color.Red); //   b=parametro / operatore lambda / corpo del metodo
buttons.ForEach(b=> {  /* corpo del metodo */  });

//-------------------------------------------------------------------------------------------------------

Action <Button> Changed;
Changed += ()=> { /* corpo del metodo */ };

//-------------------------------------------------------------------------------------------------------

buttons.Find( b => b.color == Color.Red);

bool Foo( Button b)
{
return b.color == Color.Red;
}

Find(func<Button, bool f4>); // func<Button, bool f4> = Predicate p

//-------------------------------------------------------------------------------------------------------

private class  Button{
public Image icon;
public Color color;
}

private class ListExample()
{
private List<int> numbers;
private List<Buttons> buttons
    
private void Foo(){
int find = numbers.Find(match:n => null==5);
if(numbers.Exists(n => null == 5)) { }
if(numbers.Contains(5)) { }
        
if(buttons.Exists(b => b.color == Color.Red)) { }
buttons.RemoveAll(b => b.color == Color.Red);
}
}
#endregion

#region Strings

// 		Concatenazione: string1+string2
// 		Formattazione: stringFormat("Ho {0} cani e {1} gatti", dogs, cats);

#endregion

#region Generics

public abstract class Enemy() : MonoBehaviour
{
	public float hp;
}

T GetComponent<T>();

List<GameObject> GetEnemies<T>(){
    List<GameObject> list = new list<GameObject>();
    list = GameObject.FindObjectsOfType<T>().ToList();
    return list;
}

private IEnumerable<T> GetEnemies<T>( int minimumHp) where T : Enemy {
    T[] list = Object.FindObjectsOfType<T>().ToList().Where(e => e.hp >= minimumHp);
    return list;
}

#endregion

