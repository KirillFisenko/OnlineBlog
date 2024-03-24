import { BrowserRouter, Routes, Route } from 'react-router-dom';

import UserProfile from './components/UserProfile';

import UserPublicView from './components/UserPublicView';
import SearchUser from './components/SearchUser';

import { NewsForUser } from "./components/News";
import Login from "./components/Login";

import SignUp from "./components/SignUp";



function App() {
  return (
    <BrowserRouter>
      <Routes>
        {<Route path="/" element={<Login />} />}
        <Route path='login' element={<Login />} />
        <Route path='profile' element={<UserProfile />} />
        <Route path='signup' element={<SignUp />} />
        <Route path='all/:userId' element={<UserPublicView />} />
        <Route path='all' element={<SearchUser />} />
        <Route path='/allnews' element={<NewsForUser />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;