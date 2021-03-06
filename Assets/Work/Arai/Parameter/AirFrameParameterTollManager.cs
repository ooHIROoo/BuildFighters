﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AirFrameParameterTollManager : MonoBehaviour
{
    const string JSON_PATH = "/TempResources/Json/";

    [SerializeField]
    string JSON_FILE_NAME = "";


    public float GetMass(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].mass_ : ErrorMessageFloat(id);
    }

    public float GetDrag(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].drag_ : ErrorMessageFloat(id);
    }

    public float GetMoveSpeed(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].move_speed_ : ErrorMessageFloat(id);
    }

    public float GetSwingSpeed(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].swing_speed_ : ErrorMessageFloat(id);
    }

    public float GetBoostPower(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].boost_power_ : ErrorMessageFloat(id);
    }

    public float GetJumpPower(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].jump_power_ : ErrorMessageFloat(id);
    }

    public float GetMaxBoostValue(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].max_boost_value_ : ErrorMessageFloat(id);
    }

    public float GetMaxHP(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].max_hp_ : ErrorMessageFloat(id);
    }

    public float GetArmorValue(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].armor_value_ : ErrorMessageFloat(id);
    }

    public string GetName(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].name_ : ErrorMessageString(id);
    }

    public string GetExplanatoryText(int id)
    {
        return parameters_.ContainsKey(id) ?
            parameters_[id].explanatory_text_ : ErrorMessageString(id);
    }

    void Awake()
    {
        var json_text = File.ReadAllText(Application.dataPath + JSON_PATH + JSON_FILE_NAME);

        JsonNode json = JsonNode.Parse(json_text);

        var parameters = json["Parameters"];

        foreach (var parameter in parameters)
        {
            Parameter temp_parameter;

            temp_parameter.id_ = (int)parameter["ID"].Get<long>();
            temp_parameter.mass_ = (float)parameter["Mass"].Get<double>();
            temp_parameter.drag_ = (float)parameter["Drag"].Get<double>();
            temp_parameter.move_speed_ = (float)parameter["MoveSpeed"].Get<double>();
            temp_parameter.swing_speed_ = (float)parameter["SwingSpeed"].Get<double>();
            temp_parameter.boost_power_ = (float)parameter["BoostPower"].Get<double>();
            temp_parameter.jump_power_ = (float)parameter["JumpPower"].Get<double>();
            temp_parameter.max_boost_value_ = (float)parameter["MaxBoostValue"].Get<double>();
            temp_parameter.max_hp_ = (float)parameter["MaxHP"].Get<double>();
            temp_parameter.armor_value_ = (float)parameter["ArmorValue"].Get<double>();
            temp_parameter.name_ = parameter["Name"].Get<string>();
            temp_parameter.explanatory_text_ = parameter["ExplanatoryText"].Get<string>();

            parameters_.Add(temp_parameter.id_, temp_parameter);
        }

        var a = "あｆｈｄｊｓｆｈｍさｓｂｄｇさ\u3042\u3042";
        Debug.Log(a.ToString());
        Debug.Log(json_text);

        var j = MiniJSON.Json.Deserialize(json_text) as Dictionary<string, object>;

        var b = MiniJSON.Json.Serialize(j);
        var c = b.IndexOf("\\u");

        var d = b.Substring(c, 6);

        Debug.Log(b);
        Debug.Log(c);
        Debug.Log(d.ToString());

        var re = Regex.Unescape(d);

        Debug.Log(re);
        //File.WriteAllText(Application.dataPath + JSON_PATH + JSON_FILE_NAME, b.ToString());
    }

    // いい方法が思いつかない……
    float ErrorMessageFloat(int id)
    {
        Debug.Log("ID：" + id.ToString() + "は存在しません");
        return 0.0f;
    }

    string ErrorMessageString(int id)
    {
        Debug.Log("ID：" + id.ToString() + "は存在しません");
        return "error";
    }

    struct Parameter
    {
        public int id_;
        public float mass_;
        public float drag_;
        public float move_speed_;
        public float swing_speed_;
        public float boost_power_;
        public float jump_power_;
        public float max_boost_value_;
        public float max_hp_;
        public float armor_value_;
        public string name_;
        public string explanatory_text_;
    }

    Dictionary<int, Parameter> parameters_ = new Dictionary<int, Parameter>();
}
