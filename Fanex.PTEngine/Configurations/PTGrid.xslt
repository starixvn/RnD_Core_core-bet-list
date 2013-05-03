<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:strip-space elements="*" />
  <xsl:output method="text" omit-xml-declaration="yes" indent="no"/>
  <xsl:template match="/">
    var grid = {
    name: '<xsl:value-of select="PTGrid/@name" />',
    sportTypes:[
    <xsl:for-each select="PTGrid/sportTypes/sportType">
      <xsl:if test="position() > 1">,</xsl:if>
      {
      id:<xsl:value-of select="@id"/>,
      name:'<xsl:value-of select="@name"/>', <xsl:if test="@collapsed">
      collapsed:<xsl:value-of select="@collapsed"/>,
      </xsl:if>
      betTypeGroupSets:[
      <xsl:for-each select="betTypeGroupSets/betTypeGroupSet">
        <xsl:if test="position() > 1">,</xsl:if>
        {
        isLive:<xsl:value-of select="@isLive"/>, <xsl:if test="@showGroupSetLabel">
        showGroupSetLabel:<xsl:value-of select="@showGroupSetLabel"/>,
        </xsl:if>
        betTypeGroups:[
        <xsl:for-each select="betTypeGroups/betTypeGroup">          
            <xsl:if test="position() > 1">,</xsl:if>
            {
            <xsl:if test="@id">
            id:<xsl:value-of select="@id"/>,
            </xsl:if>
            betTypes:[
            <xsl:for-each select="betTypes/betType">
              <xsl:if test="not(@visible)">
                <xsl:if test="position() > 1">,</xsl:if>
                {
                id:<xsl:value-of select="@id"/>,
                name:'<xsl:value-of select="@name"/>', <xsl:if test="@minWidth">
                minWidth:<xsl:value-of select="@minWidth"/>,
                </xsl:if>
                pt:pt
                }
              </xsl:if>
            </xsl:for-each>
            ]
            }         
        </xsl:for-each>
        ]
        }
      </xsl:for-each>
      ]
      }
    </xsl:for-each>
    ]
    }
  </xsl:template>
</xsl:stylesheet>