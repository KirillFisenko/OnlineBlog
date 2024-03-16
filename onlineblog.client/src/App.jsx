import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import UserProfile from './components/UserProfile';
import SignUp from './components/SignUp';
import UserPublicView from './components/UserPublicView';
import SearchUser from './components/SearchUser';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* <Route path="/" element={<Login />} /> */}
        <Route path='login' element={<Login />} />
        <Route path='profile' element={<UserProfile />} />
        <Route path='signup' element={<SignUp />} />
        <Route path='all/:userId' element={<UserPublicView />} />
        <Route path='/all' element={<SearchUser />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;