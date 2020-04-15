
//  MyObserver.hpp
//  APM
//
//  Created by 雍鹏亮 on 2019/10/30.
//  Copyright © 2019 xianglin. All rights reserved.
//

#ifndef MyObserver_hpp
#define MyObserver_hpp

class GPMObserver{
public:
    virtual void GPMOnMarkLevelLoad(const char *sceneId) = 0;
    
    virtual void GPMOnSetQulaity(const char *quality) = 0;
    
    virtual void GPMOnLog(const char *log) = 0;

    virtual void GPMOnFpsNotify(const char *fpsString) = 0;

    virtual ~GPMObserver() {};
};


class MyObserver : public GPMObserver{
private:
    static MyObserver * m_pInst;
public:
    static MyObserver *GetInstance();
    
    void GPMOnMarkLevelLoad(const char *sceneId);

    void GPMOnSetQulaity(const char *quality);

    void GPMOnLog(const char *log);

    void GPMOnFpsNotify(const char *fpsString);
    
};

#endif /* MyObserver_hpp */
