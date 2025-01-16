import React from 'react';
import { Box, Card } from '@mui/material';
import './MainLayout.css';

const MainLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    return (
        <>
        <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    flexGrow: 1,
                    height: '100%',
                    padding: 2,
                    boxSizing: 'border-box',
                }}
            >
                <Card
                    sx={{
                        flexGrow: 1,
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'flex-start',
                        alignItems: 'stretch',
                        padding: 3,
                        boxShadow: 3,
                        overflowY: 'auto'
                    }}
                >
                    {children}
                </Card>
            </Box>

        </>
    );
};

export default MainLayout;
