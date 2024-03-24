import { useState, useEffect } from "react";
import UserView from "./UserView";
import { getPublicUser, subscribeToUser } from "../services/usersService";
import { useParams } from "react-router-dom";


const UserPublicView = () => {
    const [user, setUser] = useState({
        id: 0,
        firstName: '',
        lastName: '',
        email: '',
        description: '',
        photo: '',
      });

      const params = useParams();
      const userId = params.userId;

      const subscribeClick = () => {
        subscribeToUser(userId);
      }
    
      useEffect(() => {
        const fetchUser = async () => {
          const data = await getPublicUser(userId);
          setUser(data);
        };
    
        fetchUser();
      }, []);

      return <UserView user={user} isProfile={false}/>;
}

export default UserPublicView;