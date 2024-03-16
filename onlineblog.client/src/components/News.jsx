import { useEffect, useState } from "react";
import { getNewsByUser } from "../services/newsService";
import ImageComponent from "./ImageComponent";

export const News = ({ text, imageStr, date }) => {
    return (
        <div>
            <p>{date}</p>
            <p>{text}</p>
            <ImageComponent base64String={imageStr} />
        </div>
    )
}

export const NewsByUser = ({ userId }) => {
    const [news, setNews] = useState([]);
    
    const getAllNews = async () => {
        if (userId === 0) return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect(() => {
        getAllNews();
    }, [userId]);

    return (
        <div>
            {news.map((el, key) => {
                return <News key={key} text={el.text} imageStr={el.image} date={el.postDate}/>
            })}
        </div>
    )
}
