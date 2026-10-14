from robot.api.deco import keyword

class CustomLibrary:
    ROBOT_LIBRARY_SCOPE = 'GLOBAL'
    
    @keyword('Get Tag ID')
    def get_tag_id(self, tags:list, tag_name:str):
        """
        Gets the ID of a tag by its name.
        """
        for tag in tags:
            if tag['name'] == tag_name:
                return tag['id']
        return None