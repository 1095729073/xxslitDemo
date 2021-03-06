﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_TootipManager : Singleton<Ctrl_TootipManager>
{
    [SerializeField] private GameObject Notification;
    [SerializeField] private GameObject NotificationLevel;
    [SerializeField] private GameObject ShopTootip;
    [SerializeField] private GameObject QuestTootip;
    public GameObject PickUpItem;
    public bool IsPickedItem { get; set; }
    [SerializeField] private Canvas canvas;

    //Tootip偏移
    [SerializeField] private Vector2 toolTipPosionOffset = new Vector2(10, -10);
    [SerializeField] public GameObject itemTootip;
    public bool isToolTipShow = false;

    /// <summary>
    /// 显示商店购选Tootip
    /// </summary>
    /// <param name="item"></param>
    /// <param name="position"></param>
    public void ShowShopTootip(Model_Item item, Vector2 position)
    {
        ShopTootip.SetActive(true);
        ShopTootip.GetComponent<View_BuyTootip>().BuyItem = item;
//        Tootip.GetComponent<View_ToolTip>().SetLocalPotion(position + toolTipPosionOffset);
        ShopTootip.transform.localPosition = position;
    }

    /// <summary>
    /// 隐藏商店购选
    /// </summary>
    public void HideShopTootip()
    {
        ShopTootip.SetActive(false);
    }

    /// <summary>
    /// 显示普通弹窗 金币不足 魔法不足 使用该弹窗
    /// </summary>
    /// <param name="contentStr">内容</param>
    /// <param name="showTime">显示时间</param>
    /// <param name="headStr">标题,默认是"系统提示"</param>
    public void ShowNotification(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "系统提示")
    {
        Notification.GetComponent<View_Notification>().ShowNotification(contentStr, showTime, headStr);
    }

    /// <summary>
    /// 显示升级弹窗 
    /// </summary>
    /// <param name="contentStr">内容</param>
    /// <param name="showTime">显示时间</param>
    /// <param name="headStr">标题,默认是"你升级了"</param>
    public void ShowNotificationLevel(string contentStr, float showTime = GlobalParametr.DEFAULTSHOWTIME,
        string headStr = "你升级了")
    {
        NotificationLevel.GetComponent<View_NotifcationLevel>().ShowNotification(contentStr, showTime, headStr);
    }

    /// <summary>
    /// 显示任务对话弹窗,并赋值任务对话详情
    /// </summary>
    /// <param name="quest"></param>
    public void ShowQuest(Model_Quest quest)
    {
        QuestTootip.gameObject.SetActive(true);
        QuestTootip.GetComponent<Ctrl_QuestTootip>().Quest = quest;
    }

    /// <summary>
    /// 仅仅显示任务对话 任务中与NPC谈话使用
    /// </summary>
    public void ShowQuest()
    {
        QuestTootip.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏任务对话
    /// </summary>
    public void HideQuest()
    {
        QuestTootip.gameObject.SetActive(false);
    }

    /// <summary>
    /// NPC对话弹窗 只用于NPC任务谈话
    /// </summary>
    /// <param name="questNpc">NPC</param>
    public void TalkInMission(Model_Quest.QuestNPC questNpc)
    {
        QuestTootip.GetComponent<Ctrl_QuestTootip>().TalkInMission(questNpc);
    }

    private void Update()
    {
        if (IsPickedItem)
        {
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            PickUpItem.GetComponent<Ctrl_PickUp>().SetLocalPosition(position);
        }

        if (isToolTipShow)
        {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            itemTootip.GetComponent<Ctrl_ItemTootip>().SetLocalPotion(position + toolTipPosionOffset);
        }
        else
        {
            itemTootip.GetComponent<Ctrl_ItemTootip>().HideItemTootip();
        }
    }
    /// <summary>
    ///  显示物品信息
    /// </summary>
    /// <param name="item"></param>
    public void ShowItemInfo(Model_Item item)
    {
        itemTootip.GetComponent<Ctrl_ItemTootip>().ShowItemInfo(item);
    }
}