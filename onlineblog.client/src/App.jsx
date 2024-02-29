import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import UserProfile from './components/UserProfile';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="login" element={<Login />} />
        <Route path='profile' element={<UserProfile />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;