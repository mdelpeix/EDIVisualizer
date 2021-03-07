<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsd="http://www.w3.org/2001/XMLSchema">

  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes"/>
  <xsl:param name="segmentName"></xsl:param>
  <xsl:param name="position"></xsl:param>
  <xsl:param name="masterSegment"></xsl:param>
  <xsl:param name="HeaderSegmentPosition"></xsl:param>
  <!--Pour tests unitaires-->
  <!--<xsl:variable name="segmentName">EDI_DC40</xsl:variable>
  <xsl:variable name="position">10</xsl:variable>
  <xsl:variable name="masterSegment">EDI_DC40</xsl:variable>
  <xsl:param name="HeaderSegmentPosition">64</xsl:param>-->
  <xsl:template match="/">
    <html>
      <head>
        <title>
          IDOC <xsl:value-of select="xsd:schema/xsd:element/@name"/>
        </title>
        <style type="text/css">
          .tableau{border: 1px solid #000000; width: 100%; border-spacing:0; border-collapse:collapse; text-align: right;}
          .tableau td{border: 1px solid #000000; padding: 5px;}
          .tableau th{background-color: #B0C4DE; color:#000000; border: 0px solid #000000; padding: 5px;}
          .important{background-color: #FF6600;color:#FFFFFF}
          .important a{color:#FFFFFF}
          .normal{background-color: #FFFFFF; color: #000000}
          .mandatory{color:#8B0000; font-weight:bold; text-align: center}
          .alignleft{text-align: left;}
          .alignCenter{text-align: center;}
        </style>
      </head>
      <body>
        <h1>
          IDOC <xsl:value-of select="xsd:schema/xsd:element/@name"/>
        </h1>
        <h2>
          Segment : <xsl:value-of select="//xsd:element[@name=$segmentName][1]/@name"/>
        </h2>
        <h3>
          Description : <xsl:value-of select="//xsd:element[@name=$segmentName][1]/xsd:annotation/xsd:documentation"/>
        </h3>

        <xsl:choose>
          <xsl:when test="$segmentName!=$masterSegment and $position &lt; $HeaderSegmentPosition">
            <h4 class="important">
              Service segment part
            </h4>
          </xsl:when>
          <xsl:otherwise></xsl:otherwise>
        </xsl:choose>

        <xsl:apply-templates select="//xsd:element[@name=$segmentName]" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="xsd:element">

    <table class="tableau">

      <tr class="alignCenter">
        <th>
          Name
        </th>
        <th>
          Position
        </th>
        <th>
          Length
        </th>
        <th>
          Description
        </th>
      </tr>

      <xsl:variable name="decalage">
        <xsl:choose>
          <xsl:when test="./@name=$masterSegment">
            <xsl:text>0</xsl:text>
          </xsl:when>
          <xsl:otherwise>
            <xsl:text>63</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:variable>


      <xsl:for-each select="./xsd:complexType/xsd:sequence/xsd:element[xsd:simpleType/xsd:restriction/xsd:maxLength/@value > 0]">

        <xsl:variable name="positionCalculated">
          <xsl:value-of select="sum(preceding::xsd:element[../../../@name=$segmentName]/xsd:simpleType/xsd:restriction/xsd:maxLength/@value) + $decalage"/>
        </xsl:variable>

        <xsl:choose>

          <xsl:when test="($position >= $positionCalculated+1) and ($position &lt; (($positionCalculated+1) + ./xsd:simpleType/xsd:restriction/xsd:maxLength/@value))">

            <tr class="important">
              <td class="alignleft">
                <a href="{./@name}:{$positionCalculated + 1}:{./xsd:simpleType/xsd:restriction/xsd:maxLength/@value}">
                  <xsl:value-of select="./@name"/>
                </a>
              </td>
              <td>
                <xsl:value-of select="$positionCalculated + 1"/>
              </td>
              <td>
                <xsl:value-of select="./xsd:simpleType/xsd:restriction/xsd:maxLength/@value"/>
              </td>
              <td class="alignleft">
                <xsl:value-of select="./xsd:annotation/xsd:documentation/text()"/>
              </td>
            </tr>

          </xsl:when>

          <xsl:otherwise>

            <tr class="cellule">
              <td class="alignleft">
                <a href="{./@name}:{$positionCalculated + 1}:{./xsd:simpleType/xsd:restriction/xsd:maxLength/@value}">
                  <xsl:value-of select="./@name"/>
                </a>
              </td>
              <td>
                <xsl:value-of select="$positionCalculated + 1"/>
              </td>
              <td>
                <xsl:value-of select="./xsd:simpleType/xsd:restriction/xsd:maxLength/@value"/>
              </td>
              <td class="alignleft">
                <xsl:value-of select="./xsd:annotation/xsd:documentation/text()"/>
              </td>
            </tr>

          </xsl:otherwise>

        </xsl:choose>

      </xsl:for-each>

    </table>

  </xsl:template>

</xsl:stylesheet>