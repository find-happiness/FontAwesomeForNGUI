using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

[RequireComponent(typeof(UILabel))]
[ExecuteInEditMode]
public class IconLable : MonoBehaviour 
{
	#region public member for unity
	//
    public string m_Text;

    public bool m_UpdateTextNow = true;

    public bool m_Snap = true;
	#endregion
	
	#region private member
	//
	private bool m_bInit = false;

    private UILabel m_label;
	#endregion
	
	#region private function for unity
	// Use this for initialization
	private void Start ()
	{

		m_bInit = true;
	}
	
	// Update is called once per frame
	private void Update () 
	{
		//
		if( !m_bInit )
			return;
	    if (m_UpdateTextNow)
        { 
	        UpdateText();
            m_UpdateTextNow = false;

            if (m_Snap)
            {
                m_label.MakePixelPerfect();
            }
        }
	}

    private void UpdateText()
    {
        string tex = "";
        m_label = this.GetComponent<UILabel>();
        m_Text = m_label.text;

        string[] strs = split(m_Text, "({fa-?.+?-?.+?})");

        foreach (var s in strs)
        {
            string iconsStr = GetValueAnd("{fa-", "}", s);

            if (string.IsNullOrEmpty(iconsStr))
            {
                tex += s;
                continue;
            }
            //Debug.Log(iconsStr);
            iconsStr = FontAwesomeIcons.GetIcon("fa-"+iconsStr);

            int i = Int32.Parse(iconsStr.Substring(2), System.Globalization.NumberStyles.HexNumber);

            tex += char.ConvertFromUtf32(i);
        }
        
        m_label.text = tex;
    }

    #endregion
	
	#region public function
	//
	
	#endregion
	
	#region private function
	//
    /// <summary>
    /// 获取两个字符串之间的字符
    /// </summary>
    /// <returns></returns>
    //public static string GetValueAnd(string strStart, string strEnd, string text)
    //{
    //    if (string.IsNullOrEmpty(text))
    //        return "";
    //    string regex = @"^.*" + strStart + "(?<content>.+?)" + strEnd + ".*$";
    //    Regex rgClass = new Regex(regex, RegexOptions.Singleline);
    //    Match match = rgClass.Match(text);

    //    return match.Groups["content"].Value;
    //}

    public static string GetValueAnd(string strStart, string strEnd, string text)
    {
        if (string.IsNullOrEmpty(text))
            return null;
        string regex = @"^.*" + strStart + "(?<content>.+?)" + strEnd + ".*$";
        Regex rgClass = new Regex(regex, RegexOptions.Singleline);
        Match match = rgClass.Match(text);

        return match.Groups["content"].Value;
    }


    public  static string[] split(string input, string pattern)
    {
        string[] substrings = Regex.Split(input, pattern);

        return substrings;
    }

    #endregion
}
