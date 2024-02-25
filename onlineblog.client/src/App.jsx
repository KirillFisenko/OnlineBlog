import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { PageOne, PageTwo } from './components/Pages';
import Login from './components/Login';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PageOne />} />
        <Route path="login" element={<Login />} />
        <Route path="one" element={<PageOne />} />
        <Route path="two" element={<PageTwo />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;