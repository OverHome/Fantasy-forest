using System.Collections.Generic;
/*!
Класс для объявления компонентов диалога    
*/
public class DialogNode
{
    /*! Короткое название диалога */
    public string ShortName { get; set; }
    /*! Сообщение диалога */
    public string Message { get; set; }
    /*! Список узлов в диалоговй ветке */
    public List<DialogNode> Children { get; set; }
    /*! Идентификатор действия после завершения ветки диалогов*/
    public int? ActionId { get; set; }
    /*! Имя персонажа*/
    public string Name { get; set; }
    /*! Диалоговый узел, входит в состав списка узлов*/
    public DialogNode()
    {
        Children = new List<DialogNode>();
    }
}
