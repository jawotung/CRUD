import React from 'react';
import { Routes, Route } from 'react-router-dom';
import User from '../pages/User/User.tsx';


const AppRoutes: React.FC = () => (

  <Routes>
    <Route path="/" element={<User />} />
  </Routes>

);

export default AppRoutes;
